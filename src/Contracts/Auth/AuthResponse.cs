
namespace Contracts.Auth;

public record AuthResponse(
    string Token,
    string Email,
    string UserId
);
