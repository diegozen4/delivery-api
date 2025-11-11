using Application.Interfaces;
using Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Commerces; // Added

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")] // Only Admin users can access this controller
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICommerceService _commerceService; // Added ICommerceService

    public AdminController(IUserService userService, ICommerceService commerceService) // Added ICommerceService
    {
        _userService = userService;
        _commerceService = commerceService; // Assigned ICommerceService
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

    [HttpPost("users/{userId}/assign-role")]
    public async Task<IActionResult> AssignRoleToUser(Guid userId, AssignRoleRequest request)
    {
        try
        {
            await _userService.AssignRoleToUserAsync(userId, request);
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

    [HttpPost("users/{userId}/revoke-role")]
    public async Task<IActionResult> RevokeRoleFromUser(Guid userId, RevokeRoleRequest request)
    {
        try
        {
            await _userService.RevokeRoleFromUserAsync(userId, request);
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

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilterDto filter)
    {
        try
        {
            var users = await _userService.GetUsersAsync(filter);
            return Ok(users);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserDetail(Guid userId)
    {
        try
        {
            var userDetail = await _userService.GetUserDetailAsync(userId);
            return Ok(userDetail);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("commerces")]
    public async Task<IActionResult> GetAllCommerces()
    {
        var commerces = await _commerceService.GetAllCommercesAsync();
        return Ok(commerces);
    }

    [HttpGet("commerces/{commerceId}")]
    public async Task<IActionResult> GetCommerceById(Guid commerceId)
    {
        try
        {
            var commerce = await _commerceService.GetCommerceByIdAsync(commerceId);
            return Ok(commerce);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("commerces")]
    public async Task<IActionResult> CreateCommerce(CreateCommerceRequest request)
    {
        try
        {
            var commerceDto = await _commerceService.CreateCommerceAsync(request);
            return CreatedAtAction(nameof(GetCommerceById), new { commerceId = commerceDto.Id }, commerceDto);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
    }

    [HttpPut("commerces/{commerceId}")]
    public async Task<IActionResult> UpdateCommerce(Guid commerceId, UpdateCommerceRequest request)
    {
        try
        {
            await _commerceService.UpdateCommerceAsync(commerceId, request);
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

    [HttpDelete("commerces/{commerceId}")]
    public async Task<IActionResult> DeleteCommerce(Guid commerceId)
    {
        try
        {
            await _commerceService.DeleteCommerceAsync(commerceId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("commerces/{commerceId}/assign-owner")]
    public async Task<IActionResult> AssignCommerceOwner(Guid commerceId, AssignCommerceOwnerRequest request)
    {
        try
        {
            await _commerceService.AssignCommerceOwnerAsync(commerceId, request);
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
