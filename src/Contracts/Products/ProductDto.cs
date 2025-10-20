namespace Contracts.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}