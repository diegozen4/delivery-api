using Application.Interfaces;
using Contracts.Commerces;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;

namespace Application.Services;

public class CommerceService : ICommerceService
{
    private readonly ICommerceRepository _commerceRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCommerceRequest> _createCommerceRequestValidator;
    private readonly IValidator<UpdateCommerceRequest> _updateCommerceRequestValidator;
    private readonly IValidator<AssignCommerceOwnerRequest> _assignCommerceOwnerRequestValidator;

    public CommerceService(
        ICommerceRepository commerceRepository,
        IAddressRepository addressRepository,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IMapper mapper,
        IValidator<CreateCommerceRequest> createCommerceRequestValidator,
        IValidator<UpdateCommerceRequest> updateCommerceRequestValidator,
        IValidator<AssignCommerceOwnerRequest> assignCommerceOwnerRequestValidator)
    {
        _commerceRepository = commerceRepository;
        _addressRepository = addressRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _createCommerceRequestValidator = createCommerceRequestValidator;
        _updateCommerceRequestValidator = updateCommerceRequestValidator;
        _assignCommerceOwnerRequestValidator = assignCommerceOwnerRequestValidator;
    }

    public async Task<IEnumerable<CommerceDto>> GetAllCommercesAsync()
    {
        var commerces = await _commerceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CommerceDto>>(commerces);
    }

    public async Task<CommerceDto> GetCommerceByIdAsync(Guid commerceId)
    {
        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }
        return _mapper.Map<CommerceDto>(commerce);
    }

    public async Task<CommerceDto> CreateCommerceAsync(CreateCommerceRequest request)
    {
        await _createCommerceRequestValidator.ValidateAndThrowAsync(request);

        var commerce = _mapper.Map<Commerce>(request);
        commerce.Id = Guid.NewGuid();
        
        // Ensure Address is properly mapped and has an ID
        if (request.Address != null)
        {
            commerce.Address = _mapper.Map<Address>(request.Address);
            commerce.Address.Id = Guid.NewGuid();
        }

        var createdCommerce = await _commerceRepository.AddAsync(commerce);
        return _mapper.Map<CommerceDto>(createdCommerce);
    }

    public async Task UpdateCommerceAsync(Guid commerceId, UpdateCommerceRequest request)
    {
        await _updateCommerceRequestValidator.ValidateAndThrowAsync(request);

        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }

        _mapper.Map(request, commerce);

        if (request.Address != null)
        {
            if (commerce.Address != null)
            {
                _mapper.Map(request.Address, commerce.Address);
                await _addressRepository.UpdateAsync(commerce.Address);
            }
            else
            {
                var newAddress = _mapper.Map<Address>(request.Address);
                newAddress.Id = Guid.NewGuid();
                commerce.Address = newAddress;
                await _addressRepository.AddAsync(newAddress);
            }
        }

        await _commerceRepository.UpdateAsync(commerce);
    }

    public async Task DeleteCommerceAsync(Guid commerceId)
    {
        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }
        await _commerceRepository.DeleteAsync(commerceId);
    }

    public async Task AssignCommerceOwnerAsync(Guid commerceId, AssignCommerceOwnerRequest request)
    {
        await _assignCommerceOwnerRequestValidator.ValidateAndThrowAsync(request);

        var commerce = await _commerceRepository.GetByIdAsync(commerceId);
        if (commerce == null)
        {
            throw new ArgumentException($"Commerce with ID {commerceId} not found.");
        }

        var user = await _userManager.FindByIdAsync(request.OwnerUserId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"User with ID {request.OwnerUserId} not found.");
        }

        // Check if user is already a "Negocio" role, if not, assign it
        if (!await _userManager.IsInRoleAsync(user, "Negocio"))
        {
            if (!await _userManager.IsInRoleAsync(user, "Administrador")) // Admins can also own commerces
            {
                if (!await _roleManager.RoleExistsAsync("Negocio"))
                {
                    await _roleManager.CreateAsync(new Role { Name = "Negocio" });
                }
                var result = await _userManager.AddToRoleAsync(user, "Negocio");
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to assign 'Negocio' role to user {user.Id}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        commerce.UserId = request.OwnerUserId;
        await _commerceRepository.UpdateAsync(commerce);
    }
}
