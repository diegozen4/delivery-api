using System.ComponentModel.DataAnnotations;

namespace Contracts.Users;

public class ApplyAsDeliveryUserRequest
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string VehicleDetails { get; set; } // Ej: "Moto Honda CB125F, Placa ABC-123"

    // Podríamos añadir más campos como tipo de licencia, etc.
}
