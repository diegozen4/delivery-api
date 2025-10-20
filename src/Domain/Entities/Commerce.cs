
namespace Domain.Entities;

public class Commerce : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
