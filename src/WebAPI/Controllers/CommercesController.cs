using Application.Interfaces;
using Contracts.Commerces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}
