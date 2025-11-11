using Contracts.Users;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserProfileDto> GetUserProfileAsync(Guid userId);
    Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileRequest request);
}
