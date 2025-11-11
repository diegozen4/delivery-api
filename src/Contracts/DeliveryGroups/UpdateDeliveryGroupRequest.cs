using System.ComponentModel.DataAnnotations;

namespace Contracts.DeliveryGroups;

public class UpdateDeliveryGroupRequest
{
    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}
