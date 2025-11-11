using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid CommerceId { get; set; } // Foreign key for Commerce
    public Commerce Commerce { get; set; } // Navigation property

    public ICollection<Product> Products { get; set; } = new List<Product>();
}