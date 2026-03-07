namespace DeliveryApi.Models;

public class EstadoPedido
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;

    public ICollection<Pedido> Pedidos { get; set; } = [];
}
