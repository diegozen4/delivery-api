using FluentValidation;
using Contracts.Users;

namespace Application.Users.Commands.AddressManagement;

public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
{
    public UpdateAddressRequestValidator()
    {
        RuleFor(x => x.Street)
            .Length(3, 200).When(x => !string.IsNullOrEmpty(x.Street))
            .WithMessage("Street must be between 3 and 200 characters.");

        RuleFor(x => x.City)
            .Length(2, 100).When(x => !string.IsNullOrEmpty(x.City))
            .WithMessage("City must be between 2 and 100 characters.");

        RuleFor(x => x.State)
            .Length(2, 100).When(x => !string.IsNullOrEmpty(x.State))
            .WithMessage("State must be between 2 and 100 characters.");

        RuleFor(x => x.ZipCode)
            .Length(3, 20).When(x => !string.IsNullOrEmpty(x.ZipCode))
            .WithMessage("ZipCode must be between 3 and 20 characters.");

        RuleFor(x => x.Country)
            .Length(2, 100).When(x => !string.IsNullOrEmpty(x.Country))
            .WithMessage("Country must be between 2 and 100 characters.");
    }
}
