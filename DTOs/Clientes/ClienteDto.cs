namespace DeliveryApi.DTOs.Clientes;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Telefono { get; set; }
    public List<DireccionDto> Direcciones { get; set; } = [];
}

public class DireccionDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Direccion_ { get; set; } = null!;
}