using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents an offer made by a delivery person for a specific order in a negotiation model.
/// </summary>
public class DeliveryOffer : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public Guid DeliveryUserId { get; set; }
    public User DeliveryUser { get; set; }

    /// <summary>
    /// The amount the delivery person is offering to complete the delivery for.
    /// Can be a counter-offer to the commerce's proposed fee.
    /// </summary>
    public decimal OfferAmount { get; set; }

    public OfferStatus Status { get; set; }
}
