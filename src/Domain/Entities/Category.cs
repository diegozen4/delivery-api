
using Domain.Entities;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Commerce> Commerces { get; set; } = new List<Commerce>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
