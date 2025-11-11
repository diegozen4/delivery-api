using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Deliveries;

public class UpdateDeliveryStatusRequest
{
    [Required]
    public OrderStatus NewStatus { get; set; }
}
