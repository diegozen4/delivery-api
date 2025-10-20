namespace Contracts.Commerces;

public class CommerceDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public Guid UserId { get; set; }
}