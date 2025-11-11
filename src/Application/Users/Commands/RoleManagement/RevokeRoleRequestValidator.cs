using FluentValidation;
using Contracts.Users;

namespace Application.Users.Commands.RoleManagement;

public class RevokeRoleRequestValidator : AbstractValidator<RevokeRoleRequest>
{
    public RevokeRoleRequestValidator()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required.")
            .Length(3, 50).WithMessage("Role name must be between 3 and 50 characters.");
    }
}
