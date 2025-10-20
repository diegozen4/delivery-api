
namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Guid CommerceId { get; set; }
    public Commerce Commerce { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
