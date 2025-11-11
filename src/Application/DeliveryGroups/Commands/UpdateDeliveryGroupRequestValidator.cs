using FluentValidation;
using Contracts.DeliveryGroups;

namespace Application.DeliveryGroups.Commands;

public class UpdateDeliveryGroupRequestValidator : AbstractValidator<UpdateDeliveryGroupRequest>
{
    public UpdateDeliveryGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100).When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description cannot exceed 500 characters.");
    }
}
