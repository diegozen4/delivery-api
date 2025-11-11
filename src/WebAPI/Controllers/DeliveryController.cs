using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Contracts.Deliveries;
using Application.Interfaces;

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
    }
}
