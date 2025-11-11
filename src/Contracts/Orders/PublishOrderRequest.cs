using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Orders;

public class PublishOrderRequest
{
    [Required]
    public DispatchMode DispatchMode { get; set; }

    /// <summary>
    /// The proposed fee for the delivery. Required when DispatchMode is Negotiation.
    /// </summary>
    public decimal? ProposedDeliveryFee { get; set; }
}
