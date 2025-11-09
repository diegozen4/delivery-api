using Application.Interfaces;
using Contracts.Products;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICommerceRepository _commerceRepository;

    public ProductService(IProductRepository productRepository, ICommerceRepository commerceRepository)
    {
        _productRepository = productRepository;
        _commerceRepository = commerceRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CommerceId = p.CommerceId
        });
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return null;
        }
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CommerceId = product.CommerceId
        };
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request, Guid userId, IEnumerable<string> userRoles)
    {
        var isOwner = await _commerceRepository.IsUserOwnerAsync(request.CommerceId, userId);
        if (!isOwner && !userRoles.Contains("Admin"))
        {
            throw new UnauthorizedAccessException("User is not authorized to create products for this commerce.");
        }

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId,
            CommerceId = request.CommerceId
        };

        var createdProduct = await _productRepository.AddAsync(product);
        return new ProductDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            CategoryId = createdProduct.CategoryId,
            CommerceId = createdProduct.CommerceId
        };
    }

    public async Task UpdateAsync(Guid id, UpdateProductRequest request, Guid userId, IEnumerable<string> userRoles)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            // Consider throwing a NotFoundException here
            return;
        }
        
        var isOwner = await _commerceRepository.IsUserOwnerAsync(product.CommerceId, userId);
        if (!isOwner && !userRoles.Contains("Admin"))
        {
            throw new UnauthorizedAccessException("User is not authorized to update products for this commerce.");
        }
        
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(Guid id, Guid userId, IEnumerable<string> userRoles)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return;
        }

        var isOwner = await _commerceRepository.IsUserOwnerAsync(product.CommerceId, userId);
        if (!isOwner && !userRoles.Contains("Admin"))
        {
            throw new UnauthorizedAccessException("User is not authorized to delete products for this commerce.");
        }

        await _productRepository.DeleteAsync(id);
    }
}