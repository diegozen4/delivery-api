using Xunit;
using Moq;
using Application.Interfaces;
using Application.Services;
using Contracts.Orders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICommerceRepository> _mockCommerceRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCommerceRepository = new Mock<ICommerceRepository>();
            _orderService = new OrderService(
                _mockOrderRepository.Object,
                _mockProductRepository.Object,
                _mockCommerceRepository.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnOrderDto_WhenOrderIsCreatedSuccessfully()
        {
            // Arrange
            var commerceId = Guid.NewGuid();
            var clientId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var productName = "Test Product";
            var productPrice = 10.0m;

            var request = new CreateOrderRequest(
                commerceId,
                new List<OrderItemRequest>
                {
                    new(productId, 2)
                });

            var mockCommerce = new Commerce { Id = commerceId, Name = "Test Commerce" };
            var mockProduct = new Product { Id = productId, Name = productName, Price = productPrice, CommerceId = commerceId };

            _mockCommerceRepository.Setup(r => r.GetByIdAsync(commerceId)).ReturnsAsync(mockCommerce);
            _mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(mockProduct);
            _mockOrderRepository.Setup(r => r.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.CreateOrderAsync(request, clientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commerceId, result.CommerceId);
            Assert.Equal(clientId, result.ClientId);
            Assert.Equal(OrderStatus.Pending.ToString(), result.Status);
            Assert.Equal(productPrice * 2, result.TotalPrice);
            Assert.Single(result.Items);
            Assert.Equal(productId, result.Items[0].ProductId);
            Assert.Equal(productName, result.Items[0].ProductName);
            Assert.Equal(2, result.Items[0].Quantity);
            Assert.Equal(productPrice, result.Items[0].Price);

            _mockOrderRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}