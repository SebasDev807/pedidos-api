namespace DeliveryApi.Models;

public class Pedido
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public string DireccionEntrega { get; set; } = null!;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaEntrega { get; set; }

    //FK a cliente
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    //FK a Usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    //FK a EstadoPedido
    public int EstadoPedidoId { get; set; }
    public EstadoPedido EstadoPedido {get; set;} = null!;

    public ICollection<DetallePedido> DetallesPedido {get; set;} = [];
}