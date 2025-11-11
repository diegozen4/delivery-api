using System;
using System.Collections.Generic;
using Contracts.Users; // For UserListItemDto

namespace Contracts.DeliveryGroups;

public class DeliveryGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CommerceId { get; set; }
    public string CommerceName { get; set; }
    public ICollection<UserListItemDto> DeliveryUsers { get; set; } = new List<UserListItemDto>();
}
