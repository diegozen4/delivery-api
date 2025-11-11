using Application.Interfaces;
using Contracts.Categories;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICommerceRepository _commerceRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryRequest> _createCategoryRequestValidator;
    private readonly IValidator<UpdateCategoryRequest> _updateCategoryRequestValidator;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ICommerceRepository commerceRepository,
        IMapper mapper,
        IValidator<CreateCategoryRequest> createCategoryRequestValidator,
        IValidator<UpdateCategoryRequest> updateCategoryRequestValidator)
    {
        _categoryRepository = categoryRepository;
        _commerceRepository = commerceRepository;
        _mapper = mapper;
        _createCategoryRequestValidator = createCategoryRequestValidator;
        _updateCategoryRequestValidator = updateCategoryRequestValidator;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByCommerceIdAsync(Guid commerceId)
    {
        var categories = await _categoryRepository.GetCategoriesByCommerceIdAsync(commerceId);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid commerceId, Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.CommerceId != commerceId)
        {
            throw new ArgumentException($"Category with ID {categoryId} not found in commerce {commerceId}.");
        }
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(Guid commerceId, CreateCategoryRequest request)
    {
        await _createCategoryRequestValidator.ValidateAndThrowAsync(request);

        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }

        var category = _mapper.Map<Category>(request);
        category.Id = Guid.NewGuid();
        category.CommerceId = commerceId;

        var createdCategory = await _categoryRepository.AddAsync(category);
        return _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task UpdateCategoryAsync(Guid commerceId, Guid categoryId, UpdateCategoryRequest request)
    {
        await _updateCategoryRequestValidator.ValidateAndThrowAsync(request);

        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.CommerceId != commerceId)
        {
            throw new ArgumentException($"Category with ID {categoryId} not found in commerce {commerceId}.");
        }

        _mapper.Map(request, category);
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(Guid commerceId, Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.CommerceId != commerceId)
        {
            throw new ArgumentException($"Category with ID {categoryId} not found in commerce {commerceId}.");
        }

        await _categoryRepository.DeleteAsync(categoryId);
    }
}