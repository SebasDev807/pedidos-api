using System.ComponentModel.DataAnnotations;

namespace DeliveryApi.DTOs.Clientes;

public class CrearDireccionDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [MaxLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres")]
    public string Direccion_ { get; set; } = null!;
}