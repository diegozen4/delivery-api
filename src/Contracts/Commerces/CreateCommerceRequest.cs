namespace Contracts.Commerces;

public class CreateCommerceRequest
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public Guid UserId { get; set; }
}