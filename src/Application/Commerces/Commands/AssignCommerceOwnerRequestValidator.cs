using FluentValidation;
using Contracts.Commerces;

namespace Application.Commerces.Commands;

public class AssignCommerceOwnerRequestValidator : AbstractValidator<AssignCommerceOwnerRequest>
{
    public AssignCommerceOwnerRequestValidator()
    {
        RuleFor(x => x.OwnerUserId)
            .NotEmpty().WithMessage("Owner User ID is required.");
    }
}
