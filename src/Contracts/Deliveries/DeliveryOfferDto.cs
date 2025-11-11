namespace Contracts.Deliveries;

public class DeliveryOfferDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid DeliveryUserId { get; set; }
    public decimal OfferAmount { get; set; }
    public string Status { get; set; }
}
