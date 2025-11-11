using System.ComponentModel.DataAnnotations;

namespace Contracts.Deliveries;

public class CreateOfferRequest
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Offer amount must be greater than 0.")]
    public decimal OfferAmount { get; set; }
}
