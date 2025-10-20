
namespace Domain.Entities;

public class DeliveryGroup : BaseEntity
{
    public string Name { get; set; }
    public Guid CommerceId { get; set; }
    public Commerce Commerce { get; set; }

    public ICollection<DeliveryGroupUser> DeliveryGroupUsers { get; set; } = new List<DeliveryGroupUser>();
}
