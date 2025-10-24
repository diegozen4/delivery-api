
using Domain.Enums;

namespace Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
