
namespace Domain.Entities;

public class DeliveryCandidate : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string ApplicationStatus { get; set; } // e.g., Pending, Approved, Rejected
    public string? RejectionReason { get; set; }
    public DateTime ApplicationDate { get; set; }
    public string? Notes { get; set; }
}
