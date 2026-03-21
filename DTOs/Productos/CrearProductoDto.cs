using System.ComponentModel.DataAnnotations;

namespace DeliveryApi.DTOs.Productos;

public class CrearProductoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = null!;

    [MaxLength(300, ErrorMessage = "La descripción no puede tener más de 300 caracteres")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }
}