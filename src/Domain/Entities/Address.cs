
namespace Domain.Entities;

public class Address : BaseEntity
{
    // 1. Ubicación Principal - Línea de Calle (Requerida)
    // Para Perú: e.g., "Av. Pardo 580"
    public string Street { get; set; } 
    
    // 2. Línea Secundaria (Opcional)
    // Para Perú: e.g., "Manzana A, Lote 12" o "Urb. Las Flores"
    public string? SecondaryAddressLine { get; set; } 

    // 3. Nivel de Ciudad (Provincia)
    // Para Perú: e.g., "Lima", "Arequipa"
    public string City { get; set; } 
    
    // 4. Nivel Local (Distrito / Localidad) - ¡Clave para Perú!
    // Para Perú: e.g., "Miraflores", "Yanahuara"
    public string? District { get; set; } 

    // 5. Nivel de Estado/Región/Departamento
    // Para Perú: e.g., "Lima" (Departamento), "La Libertad"
    public string StateOrRegion { get; set; } 
    
    // 6. Código Postal (Mantenido por Globalidad)
    // Para Perú: e.g., "15074" (Opcional en muchos casos)
    public string? ZipCode { get; set; } 

    // 7. País (Obligatorio para la Globalidad)
    public string Country { get; set; } 

    // 8. Unidad/Apartamento (Opcional, útil para edificios)
    // Para Perú: e.g., "Dpto. 401"
    public string? ApartmentNumber { get; set; } 

    // 9. Notas/Referencias (Crucial para entregas en Perú)
    // e.g., "Al lado de la bodega 'Doña Tita', tocar timbre rojo"
    public string? NotesOrReference { get; set; } 

    // --- Geolocalización (¡NUEVO!) ---
    // Para rutas, mapas y cálculos de distancia.
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // --- Relaciones ---
    public Guid UserId { get; set; }
    public User User { get; set; }
}
