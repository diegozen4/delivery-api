using FluentValidation;
using Contracts.Deliveries;
using Domain.Enums;

namespace Application.Deliveries.Commands.UpdateDeliveryStatus;

public class UpdateDeliveryStatusRequestValidator : AbstractValidator<UpdateDeliveryStatusRequest>
{
    public UpdateDeliveryStatusRequestValidator()
    {
        RuleFor(x => x.NewStatus)
            .IsInEnum()
            .WithMessage("Invalid OrderStatus specified.");
    }
}
