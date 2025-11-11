using System;
using System.Collections.Generic;

namespace Contracts.Users;

public class UserDetailDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}
