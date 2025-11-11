using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.DeliveryGroups;

public class AssignDeliveryUserToGroupRequest
{
    [Required]
    public Guid DeliveryUserId { get; set; }
}
