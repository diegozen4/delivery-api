namespace Domain.Enums;

/// <summary>
/// Defines the dispatch model for delivering an order.
/// </summary>
public enum DispatchMode
{
    /// <summary>
    /// Order is available in an open market for the first available driver to accept.
    /// </summary>
    Market,
    
    /// <summary>
    /// Order is available for drivers to bid on. The commerce chooses the winning bid.
    /// </summary>
    Negotiation
}
