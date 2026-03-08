namespace DeliveryApi.Repositories.Interfaces;

using DeliveryApi.Models;

public interface IDireccionRepository : IGenericRepository<Direccion>
{
    Task<IEnumerable<Direccion>> GetByClienteIdAsync(int clienteId);
}