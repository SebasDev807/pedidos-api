namespace DeliveryApi.DTOs.Productos;

public class CrearProductoDto
{
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
}