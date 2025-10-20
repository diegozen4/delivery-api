using Contracts.Auth;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterUserRequest request);
    Task<AuthResponse> LoginAsync(LoginUserRequest request);
    Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
