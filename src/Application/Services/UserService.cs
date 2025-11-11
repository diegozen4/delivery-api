using Application.Interfaces;
using Contracts.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateUserProfileRequest> _updateUserProfileRequestValidator;

    public UserService(
        UserManager<User> userManager,
        IMapper mapper,
        IValidator<UpdateUserProfileRequest> updateUserProfileRequestValidator)
    {
        _userManager = userManager;
        _mapper = mapper;
        _updateUserProfileRequestValidator = updateUserProfileRequestValidator;
    }

    public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} not found.");
        }
        return _mapper.Map<UserProfileDto>(user);
    }

    public async Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileRequest request)
    {
        await _updateUserProfileRequestValidator.ValidateAndThrowAsync(request);

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} not found.");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.CurrentLatitude = request.CurrentLatitude;
        user.CurrentLongitude = request.CurrentLongitude;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to update user profile: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}
