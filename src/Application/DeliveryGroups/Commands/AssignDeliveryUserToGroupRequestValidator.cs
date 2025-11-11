using FluentValidation;
using Contracts.DeliveryGroups;

namespace Application.DeliveryGroups.Commands;

public class AssignDeliveryUserToGroupRequestValidator : AbstractValidator<AssignDeliveryUserToGroupRequest>
{
    public AssignDeliveryUserToGroupRequestValidator()
    {
        RuleFor(x => x.DeliveryUserId)
            .NotEmpty().WithMessage("Delivery User ID is required.");
    }
}
