namespace Contracts.Deliveries;

public class AvailableOrderDto
{
    public Guid OrderId { get; set; }
    public string CommerceName { get; set; }
    public string CommerceAddress { get; set; }
    public string ClientAddress { get; set; }
    public decimal TotalAmount { get; set; }
}
