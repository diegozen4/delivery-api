using FluentValidation;
using Contracts.Orders;
using Domain.Enums;

namespace Application.Orders.Commands.PublishOrder;

public class PublishOrderRequestValidator : AbstractValidator<PublishOrderRequest>
{
    public PublishOrderRequestValidator()
    {
        RuleFor(x => x.DispatchMode)
            .IsInEnum()
            .WithMessage("Invalid DispatchMode specified.");

        RuleFor(x => x.ProposedDeliveryFee)
            .NotNull()
            .GreaterThan(0)
            .When(x => x.DispatchMode == DispatchMode.Negotiation)
            .WithMessage("ProposedDeliveryFee is required and must be greater than 0 for Negotiation dispatch mode.");
    }
}
