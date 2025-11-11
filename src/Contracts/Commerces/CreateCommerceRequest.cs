using System.ComponentModel.DataAnnotations;
using Contracts.Users; // Para CreateAddressRequest

namespace Contracts.Commerces;

public class CreateCommerceRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public CreateAddressRequest Address { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
