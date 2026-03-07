namespace DeliveryApi.Models;

public class DetallePedido
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }

    //FK con pedido
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;


    //fk con producto
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
}