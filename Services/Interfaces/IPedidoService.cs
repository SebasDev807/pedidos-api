namespace DeliveryApi.Services.Interfaces;

using DeliveryApi.Models;

public interface IPedidoService
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(int id);
    Task<Pedido> CreateAsync(int clienteId, int usuarioId, string direccionEntrega, List<(int ProductoId, int Cantidad)> items);
    Task UpdateEstadoAsync(int pedidoId, int nuevoEstadoId);
}