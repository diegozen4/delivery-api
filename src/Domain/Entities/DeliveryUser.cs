
using Domain.Enums;

namespace Domain.Entities;

public class DeliveryUser : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public new DeliveryUserStatus Status { get; set; }
    public string VehicleDetails { get; set; }
    public double CurrentLatitude { get; set; }
    public double CurrentLongitude { get; set; }
    public bool IsActive { get; set; }

    public ICollection<DeliveryGroupUser> DeliveryGroups { get; set; } = new List<DeliveryGroupUser>();
}
