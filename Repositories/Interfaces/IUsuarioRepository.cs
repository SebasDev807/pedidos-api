namespace DeliveryApi.Repositories.Interfaces;

using DeliveryApi.Models;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}