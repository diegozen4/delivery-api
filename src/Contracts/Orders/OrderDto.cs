using System;
using System.Collections.Generic;

namespace Contracts.Orders;

public record OrderDto(
    Guid Id,
    Guid CommerceId,
    Guid ClientId,
    List<OrderItemDto> Items,
    decimal TotalPrice,
    string Status,
    DateTime CreatedAt
);
