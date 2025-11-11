using System;

namespace Contracts.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Guid CommerceId { get; set; }
    public string CommerceName { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public bool IsAvailable { get; set; }
}
