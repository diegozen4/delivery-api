using FluentValidation;
using Contracts.Users;

namespace Application.Users.Commands.AddressManagement;

public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
{
    public CreateAddressRequestValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .Length(3, 200).WithMessage("Street must be between 3 and 200 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .Length(2, 100).WithMessage("City must be between 2 and 100 characters.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .Length(2, 100).WithMessage("State must be between 2 and 100 characters.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("ZipCode is required.")
            .Length(3, 20).WithMessage("ZipCode must be between 3 and 20 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .Length(2, 100).WithMessage("Country must be between 2 and 100 characters.");
    }
}
