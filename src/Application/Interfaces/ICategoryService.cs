using Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesByCommerceIdAsync(Guid commerceId);
    Task<CategoryDto> GetCategoryByIdAsync(Guid commerceId, Guid categoryId);
    Task<CategoryDto> CreateCategoryAsync(Guid commerceId, CreateCategoryRequest request);
    Task UpdateCategoryAsync(Guid commerceId, Guid categoryId, UpdateCategoryRequest request);
    Task DeleteCategoryAsync(Guid commerceId, Guid categoryId);
}