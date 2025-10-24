
namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public new string Status { get; set; } // e.g., Pending, Succeeded, Failed
    public string? ProviderTransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
}
