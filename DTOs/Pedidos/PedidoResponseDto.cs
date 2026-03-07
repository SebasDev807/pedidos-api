namespace DeliveryApi.DTOs.Pedidos;

public class PedidoResponseDto
{
    public int Id { get; set; }
    public string DireccionEntrega { get; set; } = null!;
    public decimal Total { get; set; }
    public string Estado { get; set; } = null!;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public List<DetalleResponseDto> Detalles { get; set; } = [];
}

public class DetalleResponseDto
{
    public string Producto { get; set; } = null!;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}