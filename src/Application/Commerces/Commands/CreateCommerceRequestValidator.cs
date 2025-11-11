using FluentValidation;
using Contracts.Commerces;

namespace Application.Commerces.Commands;

public class CreateCommerceRequestValidator : AbstractValidator<CreateCommerceRequest>
{
    public CreateCommerceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Address).NotNull().WithMessage("Address details are required.");
        RuleFor(x => x.Address.Street).NotEmpty().WithMessage("Address street is required.");
        RuleFor(x => x.Address.City).NotEmpty().WithMessage("Address city is required.");
        RuleFor(x => x.Address.State).NotEmpty().WithMessage("Address state is required.");
        RuleFor(x => x.Address.ZipCode).NotEmpty().WithMessage("Address zip code is required.");
        RuleFor(x => x.Address.Country).NotEmpty().WithMessage("Address country is required.");
    }
}
