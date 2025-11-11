using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Commerces;

public class AssignCommerceOwnerRequest
{
    [Required]
    public Guid OwnerUserId { get; set; }
}
