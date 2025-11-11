namespace Domain.Enums;

public enum OrderStatus
{
    Pending,
    Confirmed,
    InPreparation,
    ReadyForPickup,
    InTransit,
    Delivered,
    Cancelled
}