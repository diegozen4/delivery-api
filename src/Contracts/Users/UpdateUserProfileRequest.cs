using System.ComponentModel.DataAnnotations;

namespace Contracts.Users;

public class UpdateUserProfileRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
}
