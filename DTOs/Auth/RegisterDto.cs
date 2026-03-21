namespace DeliveryApi.DTOs.Auth;

using System.ComponentModel.DataAnnotations;
public class RegisterDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no es válido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = null!;

    [Phone(ErrorMessage = "El teléfono no es válido")]
    public string? Telefono { get; set; }
}