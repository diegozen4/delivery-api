using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Products;

public class UpdateProductRequest
{
    [StringLength(100, MinimumLength = 3)]
    public string? Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
    public decimal? Price { get; set; }

    [Url]
    public string? ImageUrl { get; set; }

    public Guid? CategoryId { get; set; }

    public bool? IsAvailable { get; set; }
}
