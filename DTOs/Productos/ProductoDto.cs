namespace DeliveryApi.DTOs.Productos;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
}