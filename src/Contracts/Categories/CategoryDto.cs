using System;

namespace Contracts.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CommerceId { get; set; }
    public string CommerceName { get; set; }
}