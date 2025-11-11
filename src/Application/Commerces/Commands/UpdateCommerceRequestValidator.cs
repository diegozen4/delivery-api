using FluentValidation;
using Contracts.Commerces;

namespace Application.Commerces.Commands;

public class UpdateCommerceRequestValidator : AbstractValidator<UpdateCommerceRequest>
{
    public UpdateCommerceRequestValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100).When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{10,15}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format.");
    }
}
