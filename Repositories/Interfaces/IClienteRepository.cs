namespace DeliveryApi.Repositories.Interfaces;

using DeliveryApi.Models;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetByUsuarioIdAsync(int usuarioId);
}