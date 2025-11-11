namespace Contracts.Users;

public class UserFilterDto
{
    public string? SearchTerm { get; set; }
    public string? Role { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
