using Application.Interfaces;
using Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")] // Only Admin users can access this controller
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("delivery-candidates")]
    public async Task<IActionResult> GetDeliveryCandidates()
    {
        var candidates = await _userService.GetDeliveryCandidatesAsync();
        return Ok(candidates);
    }

    [HttpPost("delivery-candidates/{candidateId}/approve")]
    public async Task<IActionResult> ApproveDeliveryCandidate(Guid candidateId, ApproveDeliveryUserRequest request)
    {
        try
        {
            await _userService.ApproveDeliveryCandidateAsync(candidateId, request);
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

    [HttpPost("delivery-candidates/{candidateId}/reject")]
    public async Task<IActionResult> RejectDeliveryCandidate(Guid candidateId, ApproveDeliveryUserRequest request)
    {
        try
        {
            await _userService.RejectDeliveryCandidateAsync(candidateId, request);
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
}
