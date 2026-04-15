namespace DeliveryApi.Application.UseCases.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IPedidoService
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(int id);
    Task<Pedido> CreateAsync(int clienteId, int usuarioId, string direccionEntrega, List<(int ProductoId, int Cantidad)> items);
    Task UpdateEstadoAsync(int pedidoId, int nuevoEstadoId);
}