using Application.Interfaces;
using Contracts.Commerces;
using Contracts.Products; // Added
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommercesController : ControllerBase
{
    private readonly ICommerceService _commerceService;
    private readonly IProductService _productService; // Added IProductService

    public CommercesController(ICommerceService commerceService, IProductService productService) // Added IProductService
    {
        _commerceService = commerceService;
        _productService = productService; // Assigned IProductService
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
}
