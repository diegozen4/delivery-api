using Contracts.Products;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<ProductDto> CreateAsync(CreateProductRequest request, Guid userId, IEnumerable<string> userRoles);
    Task UpdateAsync(Guid id, UpdateProductRequest request, Guid userId, IEnumerable<string> userRoles);    
    Task DeleteAsync(Guid id, Guid userId, IEnumerable<string> userRoles);
}