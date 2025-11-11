using Contracts.Users;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserProfileDto> GetUserProfileAsync(Guid userId);
    Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileRequest request);
    Task<IEnumerable<AddressDto>> GetAddressesAsync(Guid userId);
    Task<AddressDto> AddAddressAsync(Guid userId, CreateAddressRequest request);
    Task UpdateAddressAsync(Guid userId, Guid addressId, UpdateAddressRequest request);
    Task DeleteAddressAsync(Guid userId, Guid addressId);
    Task ApplyAsDeliveryUserAsync(Guid userId, ApplyAsDeliveryUserRequest request);
    Task<IEnumerable<DeliveryCandidateDto>> GetDeliveryCandidatesAsync();
    Task ApproveDeliveryCandidateAsync(Guid candidateId, ApproveDeliveryUserRequest request);
    Task RejectDeliveryCandidateAsync(Guid candidateId, ApproveDeliveryUserRequest request);
    Task AssignRoleToUserAsync(Guid userId, AssignRoleRequest request);
    Task RevokeRoleFromUserAsync(Guid userId, RevokeRoleRequest request);
}
