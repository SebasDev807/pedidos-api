namespace DeliveryApi.Domain.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetByUsuarioIdAsync(int usuarioId);
}
