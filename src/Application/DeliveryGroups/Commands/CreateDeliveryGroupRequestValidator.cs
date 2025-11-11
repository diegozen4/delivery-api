using FluentValidation;
using Contracts.DeliveryGroups;

namespace Application.DeliveryGroups.Commands;

public class CreateDeliveryGroupRequestValidator : AbstractValidator<CreateDeliveryGroupRequest>
{
    public CreateDeliveryGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}
