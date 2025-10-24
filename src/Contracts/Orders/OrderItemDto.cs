namespace Contracts.Orders;

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    string ProductName, // Denormalized for convenience
    int Quantity,
    decimal Price // Price at the time of order
);
