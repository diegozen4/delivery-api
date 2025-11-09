using System.Security.Claims;
using Application.Interfaces;
using Contracts.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = "Negocio,Admin")]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);
        
        var product = await _productService.CreateAsync(request, userId, userRoles);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Negocio,Admin")]
    public async Task<IActionResult> Update(Guid id, UpdateProductRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);

        await _productService.UpdateAsync(id, request, userId, userRoles);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Negocio,Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);

        await _productService.DeleteAsync(id, userId, userRoles);
        return NoContent();
    }
}