using Application.Interfaces;
using Contracts.Products;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICommerceRepository _commerceRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProductRequest> _createProductRequestValidator;
    private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator;

    public ProductService(
        IProductRepository productRepository,
        ICommerceRepository commerceRepository,
        ICategoryRepository _categoryRepository,
        IMapper mapper,
        IValidator<CreateProductRequest> createProductRequestValidator,
        IValidator<UpdateProductRequest> updateProductRequestValidator)
    {
        _productRepository = productRepository;
        _commerceRepository = commerceRepository;
        this._categoryRepository = _categoryRepository;
        _mapper = mapper;
        _createProductRequestValidator = createProductRequestValidator;
        _updateProductRequestValidator = updateProductRequestValidator;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCommerceIdAsync(Guid commerceId)
    {
        var products = await _productRepository.GetProductsByCommerceIdAsync(commerceId);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid commerceId, Guid productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.CommerceId != commerceId)
        {
            throw new ArgumentException($"Product with ID {productId} not found in commerce {commerceId}.");
        }
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(Guid commerceId, CreateProductRequest request)
    {
        await _createProductRequestValidator.ValidateAndThrowAsync(request);

        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            throw new ArgumentException($"Category with ID {request.CategoryId} not found.");
        }

        var product = _mapper.Map<Product>(request);
        product.Id = Guid.NewGuid();
        product.CommerceId = commerceId;

        var createdProduct = await _productRepository.AddAsync(product);
        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task UpdateProductAsync(Guid commerceId, Guid productId, UpdateProductRequest request)
    {
        await _updateProductRequestValidator.ValidateAndThrowAsync(request);

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.CommerceId != commerceId)
        {
            throw new ArgumentException($"Product with ID {productId} not found in commerce {commerceId}.");
        }

        if (request.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {request.CategoryId.Value} not found.");
            }
        }

        _mapper.Map(request, product);
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(Guid commerceId, Guid productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null || product.CommerceId != commerceId)
        {
            throw new ArgumentException($"Product with ID {productId} not found in commerce {commerceId}.");
        }

        await _productRepository.DeleteAsync(productId);
    }
}
