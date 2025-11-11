using Application.Interfaces;
using Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // All authenticated users can access their own profile
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        try
        {
            var userProfile = await _userService.GetUserProfileAsync(userId);
            return Ok(userProfile);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile(UpdateUserProfileRequest request)
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        try
        {
            await _userService.UpdateUserProfileAsync(userId, request);
            return NoContent(); // 204 No Content
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

    [HttpGet("me/addresses")]
    public async Task<IActionResult> GetMyAddresses()
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        var addresses = await _userService.GetAddressesAsync(userId);
        return Ok(addresses);
    }

    [HttpPost("me/addresses")]
    public async Task<IActionResult> AddMyAddress(CreateAddressRequest request)
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        try
        {
            var addressDto = await _userService.AddAddressAsync(userId, request);
            return CreatedAtAction(nameof(GetMyAddresses), new { id = addressDto.Id }, addressDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
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

    [HttpPut("me/addresses/{addressId}")]
    public async Task<IActionResult> UpdateMyAddress(Guid addressId, UpdateAddressRequest request)
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        try
        {
            await _userService.UpdateAddressAsync(userId, addressId, request);
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

    [HttpDelete("me/addresses/{addressId}")]
    public async Task<IActionResult> DeleteMyAddress(Guid addressId)
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        try
        {
            await _userService.DeleteAddressAsync(userId, addressId);
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

    [HttpPost("me/apply-as-delivery")]
    public async Task<IActionResult> ApplyAsDeliveryUser(ApplyAsDeliveryUserRequest request)
    {
        var userClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userClaimId) || !Guid.TryParse(userClaimId, out Guid userId))
        {
            return Unauthorized();
        }

        try
        {
            await _userService.ApplyAsDeliveryUserAsync(userId, request);
            return Accepted(); // 202 Accepted
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
