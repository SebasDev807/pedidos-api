namespace DeliveryApi.Services.Implementations;

using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using DeliveryApi.Services.Interfaces;


public class PedidoService : IPedidoService


{

    private readonly IPedidoRepository _pedidoRepo;
    private readonly IProductoRepository _productoRepo;

    public PedidoService(IPedidoRepository pedidoRepo, IProductoRepository productoRepo)
    {
        _pedidoRepo = pedidoRepo;
        _productoRepo = productoRepo;
    }


    public async Task<Pedido> CreateAsync(
        int clienteId,
        int usuarioId,
        string direccionEntrega,
        List<(int ProductoId, int Cantidad)> items)
    {
        var detalles = new List<DetallePedido>();
        decimal total = 0;

        // Por cada item busca el producto y calcula el subtotal
        foreach (var item in items)
        {
            var producto = await _productoRepo.GetByIdAsync(item.ProductoId);

            if (producto == null)
                throw new Exception($"Producto {item.ProductoId} no encontrado.");

            decimal subtotal = producto.Precio * item.Cantidad;
            total += subtotal;

            detalles.Add(new DetallePedido
            {
                ProductoId = item.ProductoId,
                Cantidad = item.Cantidad,
                PrecioUnitario = producto.Precio,
                Subtotal = subtotal
            });
        }

        var pedido = new Pedido
        {
            ClienteId = clienteId,
            UsuarioId = usuarioId,
            DireccionEntrega = direccionEntrega,
            EstadoPedidoId = 1,       // Pendiente por defecto
            Total = total,
            FechaCreacion = DateTime.UtcNow,
            DetallesPedido = detalles
        };

        await _pedidoRepo.AddAsync(pedido);
        await _pedidoRepo.SaveAsync();

        return pedido;
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _pedidoRepo.GetAllAsync();
    }

    public async Task<Pedido?> GetByIdAsync(int id)
    {
        var pedido = await _pedidoRepo.getByIdWithDetailsAsync(id);
        
        if (pedido == null)
        {
            throw new Exception($"Pedido con id {id} no existe");
        }

        return pedido;
    }

    public async Task UpdateEstadoAsync(int pedidoId, int nuevoEstadoId)
    {
        var pedido = await _pedidoRepo.GetByIdAsync(pedidoId)
            ?? throw new Exception($"Pedido {pedidoId} no encontrado");
        
        pedido.EstadoPedidoId = nuevoEstadoId;
        await _pedidoRepo.UpdateAsync(pedido);
        await _pedidoRepo.SaveAsync();
    }
}