using System;
using Domain.Enums;

namespace Domain.Entities;

public class DeliveryCandidate : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string VehicleDetails { get; set; }
    public new ApplicationStatus Status { get; set; } // Oculta BaseEntity.Status
    public DateTime AppliedDate { get; set; }
    public string? AdminNotes { get; set; }
}