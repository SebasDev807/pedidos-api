namespace DeliveryApi.Domain.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IPedidoRepository : IGenericRepository<Pedido>
{
    Task<IEnumerable<Pedido>> GetByClienteAsync(int clienteId);
    Task<Pedido?> getByIdWithDetailsAsync(int id);
}
