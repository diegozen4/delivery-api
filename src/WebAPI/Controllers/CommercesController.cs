using Application.Interfaces;
using Contracts.Commerces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommercesController : ControllerBase
{
    private readonly ICommerceService _commerceService;

    public CommercesController(ICommerceService commerceService)
    {
        _commerceService = commerceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommerceDto>>> GetAll()
    {
        var commerces = await _commerceService.GetAllAsync();
        return Ok(commerces);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommerceDto>> GetById(Guid id)
    {
        var commerce = await _commerceService.GetByIdAsync(id);
        if (commerce == null)
        {
            return NotFound();
        }
        return Ok(commerce);
    }

    [HttpPost]
    public async Task<ActionResult<CommerceDto>> Create(CreateCommerceRequest request)
    {
        var commerce = await _commerceService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = commerce.Id }, commerce);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCommerceRequest request)
    {
        await _commerceService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _commerceService.DeleteAsync(id);
        return NoContent();
    }
}