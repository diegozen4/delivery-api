
namespace Domain.Entities;

public class DeliveryGroupUser : BaseEntity
{
    public Guid DeliveryGroupId { get; set; }
    public DeliveryGroup DeliveryGroup { get; set; }

    public Guid DeliveryUserId { get; set; }
    public DeliveryUser DeliveryUser { get; set; }
}
