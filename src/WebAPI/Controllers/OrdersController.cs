using Application.Interfaces;
using Contracts.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize(Roles = "Cliente")] // Only clients can create orders
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        // Get ClientId from authenticated user
        var clientClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(clientClaimId))
        {
            return Unauthorized(); // Or BadRequest if client ID is mandatory
        }
        if (!Guid.TryParse(clientClaimId, out Guid clientId))
        {
            return Unauthorized(); // Invalid client ID format
        }

        try
        {
            var orderDto = await _orderService.CreateOrderAsync(request, clientId);
            return CreatedAtAction(nameof(CreateOrder), new { id = orderDto.Id }, orderDto);
        }
        catch (ArgumentException ex)
        {
            // Catch validation errors from the service
            return BadRequest(new { message = ex.Message });
        }
        // Other exceptions will be caught by the ErrorHandlingMiddleware
    }

    [HttpPost("{orderId}/publish")]
    [Authorize(Roles = "Negocio")] // Only businesses can publish orders
    public async Task<IActionResult> PublishOrder(Guid orderId, PublishOrderRequest request)
    {
        var ownerClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(ownerClaimId) || !Guid.TryParse(ownerClaimId, out Guid ownerId))
        {
            return Unauthorized();
        }

        try
        {
            await _orderService.PublishOrderAsync(orderId, request, ownerId);
            return NoContent(); // 204 No Content, as the order is updated
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message); // 403 Forbidden
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        // Other exceptions will be caught by the ErrorHandlingMiddleware
    }
}
