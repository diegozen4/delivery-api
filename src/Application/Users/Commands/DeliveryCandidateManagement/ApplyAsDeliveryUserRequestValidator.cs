using FluentValidation;
using Contracts.Users;

namespace Application.Users.Commands.DeliveryCandidateManagement;

public class ApplyAsDeliveryUserRequestValidator : AbstractValidator<ApplyAsDeliveryUserRequest>
{
    public ApplyAsDeliveryUserRequestValidator()
    {
        RuleFor(x => x.VehicleDetails)
            .NotEmpty().WithMessage("Vehicle details are required.")
            .Length(3, 200).WithMessage("Vehicle details must be between 3 and 200 characters.");
    }
}
