using System.ComponentModel.DataAnnotations;

namespace Contracts.Users;

public class UpdateAddressRequest
{
    [StringLength(200, MinimumLength = 3)]
    public string? Street { get; set; }

    [StringLength(100, MinimumLength = 2)]
    public string? City { get; set; }

    [StringLength(100, MinimumLength = 2)]
    public string? State { get; set; }

    [StringLength(20, MinimumLength = 3)]
    public string? ZipCode { get; set; }

    [StringLength(100, MinimumLength = 2)]
    public string? Country { get; set; }

    public bool? IsDefault { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
