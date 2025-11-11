using System;
using Domain.Enums;

namespace Contracts.Orders;

public class OrderHistoryItemDto
{
    public Guid OrderId { get; set; }
    public string CommerceName { get; set; }
    public string ClientName { get; set; }
    public string? DeliveryUserName { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DispatchMode DispatchMode { get; set; }
    public decimal? ProposedDeliveryFee { get; set; }
}
