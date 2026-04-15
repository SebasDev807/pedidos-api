namespace DeliveryApi.Domain.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}
