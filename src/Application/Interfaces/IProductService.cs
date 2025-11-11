using Contracts.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsByCommerceIdAsync(Guid commerceId);
    Task<ProductDto> GetProductByIdAsync(Guid commerceId, Guid productId);
    Task<ProductDto> CreateProductAsync(Guid commerceId, CreateProductRequest request);
    Task UpdateProductAsync(Guid commerceId, Guid productId, UpdateProductRequest request);
    Task DeleteProductAsync(Guid commerceId, Guid productId);
}
