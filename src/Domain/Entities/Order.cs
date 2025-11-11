
using Domain.Enums;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public Guid ClientId { get; set; }
    public User User { get; set; }
    public Guid CommerceId { get; set; }
    public Commerce Commerce { get; set; }
    public DateTime OrderDate { get; set; }
    public new OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    
    // Dispatch properties
    public DispatchMode DispatchMode { get; set; }
    public decimal? ProposedDeliveryFee { get; set; }

    // Delivery assignment
    public Guid? DeliveryUserId { get; set; }
    public User? DeliveryUser { get; set; }
    
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public ICollection<OrderStatusHistory> StatusHistory { get; set; } = new List<OrderStatusHistory>();
    public ICollection<DeliveryOffer> DeliveryOffers { get; set; } = new List<DeliveryOffer>();
}
