using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Commerce : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; } // Added Description
    public string PhoneNumber { get; set; } // Added PhoneNumber
    public string Email { get; set; } // Added Email
    public Guid AddressId { get; set; } // Foreign key for Address
    public Address Address { get; set; } // Navigation property
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid? UserId { get; set; } // Changed to nullable
    public User? User { get; set; } // Changed to nullable
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}