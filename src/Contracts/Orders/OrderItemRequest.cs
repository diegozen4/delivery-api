namespace Contracts.Orders;

public record OrderItemRequest(
    Guid ProductId,
    int Quantity
);
