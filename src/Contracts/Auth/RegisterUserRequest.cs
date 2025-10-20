
namespace Contracts.Auth;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);
