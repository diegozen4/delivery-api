using FluentValidation;
using Contracts.Deliveries;

namespace Application.Deliveries.Commands.CreateOffer;

public class CreateOfferRequestValidator : AbstractValidator<CreateOfferRequest>
{
    public CreateOfferRequestValidator()
    {
        RuleFor(x => x.OfferAmount)
            .GreaterThan(0)
            .WithMessage("Offer amount must be greater than 0.");
    }
}
