using System;
using Contracts.Users; // Para AddressDto

namespace Contracts.Commerces;

public class CommerceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public AddressDto Address { get; set; }
    public Guid? OwnerUserId { get; set; }
    public string? OwnerUserName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
