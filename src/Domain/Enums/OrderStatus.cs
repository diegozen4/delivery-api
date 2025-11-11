namespace Domain.Enums;

public enum OrderStatus
{
    Pending,
    Confirmed,
    InPreparation,
    ReadyForPickup, // For Market model
    AwaitingBids,   // For Negotiation model
    InTransit,
    Delivered,
    Cancelled
}