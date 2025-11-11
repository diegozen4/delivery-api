using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Contracts.Deliveries;
using Application.Interfaces;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Repartidor")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet("available-orders")]
        public async Task<IActionResult> GetAvailableOrders()
        {
            var orders = await _deliveryService.GetAvailableOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("negotiable-orders")]
        public async Task<IActionResult> GetNegotiableOrders()
        {
            var orders = await _deliveryService.GetNegotiableOrdersAsync();
            return Ok(orders);
        }

        [HttpPost("orders/{orderId}/offers")]
        public async Task<IActionResult> CreateOffer(Guid orderId, CreateOfferRequest request)
        {
            var deliveryUserClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(deliveryUserClaimId) || !Guid.TryParse(deliveryUserClaimId, out Guid deliveryUserId))
            {
                return Unauthorized();
            }

            try
            {
                await _deliveryService.CreateOfferAsync(orderId, request, deliveryUserId);
                return NoContent(); // 204 No Content, as the offer is created
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            // Other exceptions will be caught by the ErrorHandlingMiddleware
        }

        [HttpPost("orders/{orderId}/accept")]
        public async Task<IActionResult> AcceptMarketOrder(Guid orderId)
        {
            var deliveryUserClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(deliveryUserClaimId) || !Guid.TryParse(deliveryUserClaimId, out Guid deliveryUserId))
            {
                return Unauthorized();
            }

            try
            {
                await _deliveryService.AcceptMarketOrderAsync(orderId, deliveryUserId);
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            // Other exceptions will be caught by the ErrorHandlingMiddleware
        }

        [HttpPut("orders/{orderId}/status")]
        public async Task<IActionResult> UpdateDeliveryStatus(Guid orderId, UpdateDeliveryStatusRequest request)
        {
            var deliveryUserClaimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(deliveryUserClaimId) || !Guid.TryParse(deliveryUserClaimId, out Guid deliveryUserId))
            {
                return Unauthorized();
            }

            try
            {
                await _deliveryService.UpdateDeliveryStatusAsync(orderId, request, deliveryUserId);
                return NoContent(); // 204 No Content
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            // Other exceptions will be caught by the ErrorHandlingMiddleware
        }
    }
}
