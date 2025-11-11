using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsByCommerceIdAsync(Guid commerceId); // Added
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);    
    Task DeleteAsync(Guid id);
}
