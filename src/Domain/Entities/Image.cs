
namespace Domain.Entities;

public class Image : BaseEntity
{
    public string Url { get; set; }
    public string? Title { get; set; }
    public string? AltText { get; set; }

    // This allows for a polymorphic association
    public Guid OwnerId { get; set; }
    public string OwnerType { get; set; } // e.g., "Product", "User", "Commerce"
}
