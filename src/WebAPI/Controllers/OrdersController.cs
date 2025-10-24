using Application.Interfaces;
using Contracts.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

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
}
