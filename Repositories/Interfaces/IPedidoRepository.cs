namespace DeliveryApi.Repositories.Interfaces;

using DeliveryApi.Models;

public interface IPedidoRepository : IGenericRepository<Pedido>
{
    Task<IEnumerable<Pedido>> GetByClienteAsync(int clienteId);
    Task<Pedido?> getByIdWithDetailsAsync(int id);
}