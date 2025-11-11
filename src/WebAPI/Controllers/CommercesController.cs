using Application.Interfaces;
using Contracts.Commerces;
using Contracts.DeliveryGroups; // Added for DeliveryGroup DTOs and Requests
using Contracts.Products;
using Contracts.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using System.Linq;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommercesController : ControllerBase
{
    private readonly ICommerceService _commerceService;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IDeliveryGroupService _deliveryGroupService;

    public CommercesController(ICommerceService commerceService, IProductService productService, ICategoryService categoryService, IDeliveryGroupService deliveryGroupService)
    {
        _commerceService = commerceService;
        _productService = productService;
        _categoryService = categoryService;
        _deliveryGroupService = deliveryGroupService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommerceDto>>> GetAll()
    {
        var commerces = await _commerceService.GetAllCommercesAsync();
        return Ok(commerces);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommerceDto>> GetById(Guid id)
    {
        try
        {
            var commerce = await _commerceService.GetCommerceByIdAsync(id);
            return Ok(commerce);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<CommerceDto>> Create(CreateCommerceRequest request)
    {
        try
        {
            var commerce = await _commerceService.CreateCommerceAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = commerce.Id }, commerce);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCommerceRequest request)
    {
        try
        {
            await _commerceService.UpdateCommerceAsync(id, request);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _commerceService.DeleteCommerceAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Product Management Endpoints
    [HttpGet("{commerceId}/products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCommerceId(Guid commerceId)
    {
        var products = await _productService.GetProductsByCommerceIdAsync(commerceId);
        return Ok(products);
    }

    [HttpGet("{commerceId}/products/{productId}")]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid commerceId, Guid productId)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(commerceId, productId);
            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{commerceId}/products")]
    public async Task<ActionResult<ProductDto>> CreateProduct(Guid commerceId, CreateProductRequest request)
    {
        try
        {
            var product = await _productService.CreateProductAsync(commerceId, request);
            return CreatedAtAction(nameof(GetProductById), new { commerceId = commerceId, productId = product.Id }, product);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPut("{commerceId}/products/{productId}")]
    public async Task<IActionResult> UpdateProduct(Guid commerceId, Guid productId, UpdateProductRequest request)
    {
        try
        {
            await _productService.UpdateProductAsync(commerceId, productId, request);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpDelete("{commerceId}/products/{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid commerceId, Guid productId)
    {
        try
        {
            await _productService.DeleteProductAsync(commerceId, productId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Category Management Endpoints
    [HttpGet("{commerceId}/categories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesByCommerceId(Guid commerceId)
    {
        var categories = await _categoryService.GetCategoriesByCommerceIdAsync(commerceId);
        return Ok(categories);
    }

    [HttpGet("{commerceId}/categories/{categoryId}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid commerceId, Guid categoryId)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(commerceId, categoryId);
            return Ok(category);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{commerceId}/categories")]
    public async Task<ActionResult<CategoryDto>> CreateCategory(Guid commerceId, CreateCategoryRequest request)
    {
        try
        {
            var category = await _categoryService.CreateCategoryAsync(commerceId, request);
            return CreatedAtAction(nameof(GetCategoryById), new { commerceId = commerceId, categoryId = category.Id }, category);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPut("{commerceId}/categories/{categoryId}")]
    public async Task<IActionResult> UpdateCategory(Guid commerceId, Guid categoryId, UpdateCategoryRequest request)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(commerceId, categoryId, request);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpDelete("{commerceId}/categories/{categoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid commerceId, Guid categoryId)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(commerceId, categoryId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // Delivery Group Management Endpoints
    [HttpGet("{commerceId}/delivery-groups")]
    public async Task<ActionResult<IEnumerable<DeliveryGroupDto>>> GetDeliveryGroupsByCommerceId(Guid commerceId)
    {
        var deliveryGroups = await _deliveryGroupService.GetDeliveryGroupsByCommerceIdAsync(commerceId);
        return Ok(deliveryGroups);
    }

    [HttpGet("{commerceId}/delivery-groups/{groupId}")]
    public async Task<ActionResult<DeliveryGroupDto>> GetDeliveryGroupById(Guid commerceId, Guid groupId)
    {
        try
        {
            var deliveryGroup = await _deliveryGroupService.GetDeliveryGroupByIdAsync(commerceId, groupId);
            return Ok(deliveryGroup);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{commerceId}/delivery-groups")]
    public async Task<ActionResult<DeliveryGroupDto>> CreateDeliveryGroup(Guid commerceId, CreateDeliveryGroupRequest request)
    {
        try
        {
            var deliveryGroup = await _deliveryGroupService.CreateDeliveryGroupAsync(commerceId, request);
            return CreatedAtAction(nameof(GetDeliveryGroupById), new { commerceId = commerceId, groupId = deliveryGroup.Id }, deliveryGroup);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPut("{commerceId}/delivery-groups/{groupId}")]
    public async Task<IActionResult> UpdateDeliveryGroup(Guid commerceId, Guid groupId, UpdateDeliveryGroupRequest request)
    {
        try
        {
            await _deliveryGroupService.UpdateDeliveryGroupAsync(commerceId, groupId, request);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpDelete("{commerceId}/delivery-groups/{groupId}")]
    public async Task<IActionResult> DeleteDeliveryGroup(Guid commerceId, Guid groupId)
    {
        try
        {
            await _deliveryGroupService.DeleteDeliveryGroupAsync(commerceId, groupId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{commerceId}/delivery-groups/{groupId}/assign-delivery-user")]
    public async Task<IActionResult> AssignDeliveryUserToGroup(Guid commerceId, Guid groupId, AssignDeliveryUserToGroupRequest request)
    {
        try
        {
            await _deliveryGroupService.AssignDeliveryUserToGroupAsync(commerceId, groupId, request);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpDelete("{commerceId}/delivery-groups/{groupId}/remove-delivery-user/{deliveryUserId}")]
    public async Task<IActionResult> RemoveDeliveryUserFromGroup(Guid commerceId, Guid groupId, Guid deliveryUserId)
    {
        try
        {
            await _deliveryGroupService.RemoveDeliveryUserFromGroupAsync(commerceId, groupId, deliveryUserId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
