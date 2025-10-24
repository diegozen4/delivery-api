using System.Collections.Generic;

namespace Contracts.Orders;

public record CreateOrderRequest(
    Guid CommerceId,
    List<OrderItemRequest> Items
);
