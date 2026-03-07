namespace DeliveryApi.Models;

public class Direccion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Direccion_ { get; set; } = null!;
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
}
