using System.ComponentModel.DataAnnotations;
namespace DeliveryApi.DTOs.Auth;

public class LoginRequestDto
{
    [Required(ErrorMessage = "El email es obligatorio")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = null!;
}