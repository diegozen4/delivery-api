using FluentValidation;
using Contracts.Products;
using System;

namespace Application.Products.Commands;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100).When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).When(x => x.Price.HasValue)
            .WithMessage("Price must be greater than 0.")
            .LessThanOrEqualTo(10000).When(x => x.Price.HasValue)
            .WithMessage("Price cannot exceed 10000.");

        RuleFor(x => x.ImageUrl)
            .Must(LinkMustBeAUri).When(x => !string.IsNullOrEmpty(x.ImageUrl))
            .WithMessage("Image URL must be a valid URI.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().When(x => x.CategoryId.HasValue)
            .WithMessage("Category ID is required.");
    }

    private bool LinkMustBeAUri(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return false;
        }
        return Uri.TryCreate(link, UriKind.Absolute, out _);
    }
}
