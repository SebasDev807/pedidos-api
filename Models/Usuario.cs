namespace DeliveryApi.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public string Email {get; set;} = null!;
    public string Password { get; set; } = null!;

    public string Rol { get; set; } = "cliente";

    public string? Telefono { get; set; }

    //Un usuario puede tener un cliente asociado
    public Cliente? Cliente { get; set; }
}
