using System;

namespace Contracts.Users;

public class AddressDto
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public bool IsDefault { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
