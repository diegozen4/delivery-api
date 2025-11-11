using Application.Interfaces;
using Contracts.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Domain.Enums;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IAddressRepository _addressRepository;
    private readonly IDeliveryCandidateRepository _deliveryCandidateRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateUserProfileRequest> _updateUserProfileRequestValidator;
    private readonly IValidator<CreateAddressRequest> _createAddressRequestValidator;
    private readonly IValidator<UpdateAddressRequest> _updateAddressRequestValidator;
    private readonly IValidator<ApplyAsDeliveryUserRequest> _applyAsDeliveryUserRequestValidator;
    private readonly IValidator<ApproveDeliveryUserRequest> _approveDeliveryUserRequestValidator;

    public UserService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IAddressRepository addressRepository,
        IDeliveryCandidateRepository deliveryCandidateRepository,
        IMapper mapper,
        IValidator<UpdateUserProfileRequest> updateUserProfileRequestValidator,
        IValidator<CreateAddressRequest> createAddressRequestValidator,
        IValidator<UpdateAddressRequest> updateAddressRequestValidator,
        IValidator<ApplyAsDeliveryUserRequest> applyAsDeliveryUserRequestValidator,
        IValidator<ApproveDeliveryUserRequest> approveDeliveryUserRequestValidator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _addressRepository = addressRepository;
        _deliveryCandidateRepository = deliveryCandidateRepository;
        _mapper = mapper;
        _updateUserProfileRequestValidator = updateUserProfileRequestValidator;
        _createAddressRequestValidator = createAddressRequestValidator;
        _updateAddressRequestValidator = updateAddressRequestValidator;
        _applyAsDeliveryUserRequestValidator = applyAsDeliveryUserRequestValidator;
        _approveDeliveryUserRequestValidator = approveDeliveryUserRequestValidator;
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

    public async Task<IEnumerable<AddressDto>> GetAddressesAsync(Guid userId)
    {
        var addresses = await _addressRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<AddressDto>>(addresses);
    }

    public async Task<AddressDto> AddAddressAsync(Guid userId, CreateAddressRequest request)
    {
        await _createAddressRequestValidator.ValidateAndThrowAsync(request);

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} not found.");
        }

        var address = _mapper.Map<Address>(request);
        address.UserId = userId;
        address.Id = Guid.NewGuid();

        // If new address is set as default, ensure other addresses are not default
        if (address.IsDefault)
        {
            var existingAddresses = await _addressRepository.GetByUserIdAsync(userId);
            foreach (var existingAddress in existingAddresses.Where(a => a.IsDefault))
            {
                existingAddress.IsDefault = false;
                await _addressRepository.UpdateAsync(existingAddress);
            }
        }

        await _addressRepository.AddAsync(address);
        return _mapper.Map<AddressDto>(address);
    }

    public async Task UpdateAddressAsync(Guid userId, Guid addressId, UpdateAddressRequest request)
    {
        await _updateAddressRequestValidator.ValidateAndThrowAsync(request);

        var address = await _addressRepository.GetByIdAndUserIdAsync(addressId, userId);
        if (address == null)
        {
            throw new ArgumentException($"Address with ID {addressId} not found or does not belong to user {userId}.");
        }

        _mapper.Map(request, address); // Apply updates from request to address entity

        // If address is set as default, ensure other addresses are not default
        if (request.IsDefault == true && !address.IsDefault) // Only if it's becoming default
        {
            var existingAddresses = await _addressRepository.GetByUserIdAsync(userId);
            foreach (var existingAddress in existingAddresses.Where(a => a.IsDefault && a.Id != addressId))
            {
                existingAddress.IsDefault = false;
                await _addressRepository.UpdateAsync(existingAddress);
            }
        }
        address.IsDefault = request.IsDefault ?? address.IsDefault; // Update IsDefault property

        await _addressRepository.UpdateAsync(address);
    }

    public async Task DeleteAddressAsync(Guid userId, Guid addressId)
    {
        var address = await _addressRepository.GetByIdAndUserIdAsync(addressId, userId);
        if (address == null)
        {
            throw new ArgumentException($"Address with ID {addressId} not found or does not belong to user {userId}.");
        }

        if (address.IsDefault)
        {
            throw new InvalidOperationException("Cannot delete default address. Please set another address as default first.");
        }

        await _addressRepository.DeleteAsync(address);
    }

    public async Task ApplyAsDeliveryUserAsync(Guid userId, ApplyAsDeliveryUserRequest request)
    {
        await _applyAsDeliveryUserRequestValidator.ValidateAndThrowAsync(request);

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} not found.");
        }

        // Check if user already applied or is already a delivery user
        var existingCandidate = await _deliveryCandidateRepository.GetByUserIdAsync(userId);
        if (existingCandidate != null)
        {
            throw new InvalidOperationException($"User {userId} has already applied as a delivery user or has a pending application.");
        }

        var isDeliveryUser = await _userManager.IsInRoleAsync(user, "Repartidor");
        if (isDeliveryUser)
        {
            throw new InvalidOperationException($"User {userId} is already a delivery user.");
        }

        var candidate = new DeliveryCandidate
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            User = user,
            VehicleDetails = request.VehicleDetails,
            Status = ApplicationStatus.Pending,
            AppliedDate = DateTime.UtcNow
        };

        await _deliveryCandidateRepository.AddAsync(candidate);
    }

    public async Task<IEnumerable<DeliveryCandidateDto>> GetDeliveryCandidatesAsync()
    {
        var candidates = await _deliveryCandidateRepository.GetAllPendingAsync();
        return _mapper.Map<IEnumerable<DeliveryCandidateDto>>(candidates);
    }

    public async Task ApproveDeliveryCandidateAsync(Guid candidateId, ApproveDeliveryUserRequest request)
    {
        await _approveDeliveryUserRequestValidator.ValidateAndThrowAsync(request);

        var candidate = await _deliveryCandidateRepository.GetByIdAsync(candidateId);
        if (candidate == null)
        {
            throw new ArgumentException($"Delivery candidate with ID {candidateId} not found.");
        }

        if (candidate.Status != ApplicationStatus.Pending)
        {
            throw new InvalidOperationException($"Candidate {candidateId} is not in Pending status and cannot be approved.");
        }

        if (!request.Approved)
        {
            throw new InvalidOperationException("To approve, 'Approved' field in request must be true.");
        }

        var user = await _userManager.FindByIdAsync(candidate.UserId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"User associated with candidate {candidateId} not found.");
        }

        // Assign "Repartidor" role
        if (!await _roleManager.RoleExistsAsync("Repartidor"))
        {
            await _roleManager.CreateAsync(new Role { Name = "Repartidor" }); // Corregido
        }
        var result = await _userManager.AddToRoleAsync(user, "Repartidor");
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to assign 'Repartidor' role to user {user.Id}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        candidate.Status = ApplicationStatus.Approved;
        candidate.AdminNotes = request.AdminNotes;
        await _deliveryCandidateRepository.UpdateAsync(candidate);
    }

    public async Task RejectDeliveryCandidateAsync(Guid candidateId, ApproveDeliveryUserRequest request)
    {
        await _approveDeliveryUserRequestValidator.ValidateAndThrowAsync(request);

        var candidate = await _deliveryCandidateRepository.GetByIdAsync(candidateId);
        if (candidate == null)
        {
            throw new ArgumentException($"Delivery candidate with ID {candidateId} not found.");
        }

        if (candidate.Status != ApplicationStatus.Pending)
        {
            throw new InvalidOperationException($"Candidate {candidateId} is not in Pending status and cannot be rejected.");
        }

        if (request.Approved)
        {
            throw new InvalidOperationException("To reject, 'Approved' field in request must be false.");
        }

        candidate.Status = ApplicationStatus.Rejected;
        candidate.AdminNotes = request.AdminNotes;
        await _deliveryCandidateRepository.UpdateAsync(candidate);
    }
}
