using System.ComponentModel.DataAnnotations;

namespace Contracts.Users;

public class RevokeRoleRequest
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string RoleName { get; set; }
}
