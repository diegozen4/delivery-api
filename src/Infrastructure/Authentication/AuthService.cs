using Application.Interfaces;
using Contracts.Auth;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> LoginAsync(LoginUserRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ApplicationException("Invalid email or password.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new AuthResponse(token, user.Email!, user.Id.ToString());
    }

    public async Task<AuthResponse> RegisterAsync(RegisterUserRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            // FirstName and LastName from the request can be added as claims or properties if needed
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ApplicationException($"Registration failed: {errors}");
        }

        // Assign a default role, e.g., "Client"
        const string defaultRole = "Cliente";
        if (!await _roleManager.RoleExistsAsync(defaultRole))
        {
            await _roleManager.CreateAsync(new Role { Name = defaultRole, NormalizedName = defaultRole.ToUpper() });
        }
        await _userManager.AddToRoleAsync(user, defaultRole);

        var roles = new List<string> { defaultRole };
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new AuthResponse(token, user.Email!, user.Id.ToString());
    }

    public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        if (request.NewPassword != request.ConfirmNewPassword)
        {
            throw new ApplicationException("New password and confirmation password do not match.");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ApplicationException("User not found.");
        }

        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ApplicationException($"Failed to change password: {errors}");
        }

        return result.Succeeded;
    }
}
