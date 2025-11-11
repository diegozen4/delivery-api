using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetCategoriesByCommerceIdAsync(Guid commerceId); // Added
    Task<Category> AddAsync(Category category);
    Task UpdateAsync(Category category);    
    Task DeleteAsync(Guid id);
}