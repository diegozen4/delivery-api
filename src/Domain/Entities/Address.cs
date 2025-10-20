
namespace Domain.Entities;

public class Address : BaseEntity
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string? Apartment { get; set; }
    public string? Notes { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
