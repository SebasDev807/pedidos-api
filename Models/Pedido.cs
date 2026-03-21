namespace DeliveryApi.Models;

public class Pedido
{
    public int Id { get; set; }
    public decimal ValorTotal { get; set; }
    public int IdDireccionEntrega { get; set; }
    public Direccion? Direccion { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaEntrega { get; set; }

    public int IdCliente { get; set; }
    public Cliente? Cliente { get; set; }

    public int IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public int IdEstado { get; set; }
    public EstadoPedido? EstadoPedido { get; set; }

    public ICollection<DetallePedido> DetallesPedido { get; set; } = [];
}