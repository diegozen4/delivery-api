
using Domain.Enums;

namespace Domain.Entities;

public class OrderStatusHistory : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public OrderStatus Status { get; set; }
    public DateTime ChangeDate { get; set; }
    public string Notes { get; set; }
}
