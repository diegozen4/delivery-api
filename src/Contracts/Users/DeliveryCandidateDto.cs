using System;
using Domain.Enums; // Añadir esta línea

namespace Contracts.Users;

public class DeliveryCandidateDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string VehicleDetails { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime AppliedDate { get; set; }
    public string? AdminNotes { get; set; }
}
