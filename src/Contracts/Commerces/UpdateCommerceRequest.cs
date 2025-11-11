using System.ComponentModel.DataAnnotations;
using Contracts.Users; // Para UpdateAddressRequest

namespace Contracts.Commerces;

public class UpdateCommerceRequest
{
    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public UpdateAddressRequest? Address { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
