using FluentValidation;
using Contracts.Users;

namespace Application.Users.Commands.DeliveryCandidateManagement;

public class ApproveDeliveryUserRequestValidator : AbstractValidator<ApproveDeliveryUserRequest>
{
    public ApproveDeliveryUserRequestValidator()
    {
        RuleFor(x => x.Approved)
            .NotNull().WithMessage("Approved status is required.");
    }
}
