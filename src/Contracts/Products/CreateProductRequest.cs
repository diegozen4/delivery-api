using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Products;

public class CreateProductRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
    public decimal Price { get; set; }

    [Required]
    [Url]
    public string ImageUrl { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    public bool IsAvailable { get; set; } = true;
}
