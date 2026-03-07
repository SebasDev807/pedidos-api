namespace DeliveryApi.DTOs.Pedidos;

public class CrearPedidoDto
{
    public int ClienteId { get; set; }
    public string DireccionEntrega { get; set; } = null!;
    public List<PedidoItemDto> Items { get; set; } = [];
}

public class PedidoItemDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}