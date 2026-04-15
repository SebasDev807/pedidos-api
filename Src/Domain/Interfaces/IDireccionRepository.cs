namespace DeliveryApi.Domain.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IDireccionRepository : IGenericRepository<Direccion>
{
    Task<IEnumerable<Direccion>> GetByClienteIdAsync(int clienteId);
}
