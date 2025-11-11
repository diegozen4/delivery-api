using System.ComponentModel.DataAnnotations;

namespace Contracts.Users;

public class ApproveDeliveryUserRequest
{
    [Required]
    public bool Approved { get; set; }
    public string? AdminNotes { get; set; }
}
