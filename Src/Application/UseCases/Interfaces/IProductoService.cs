namespace DeliveryApi.Application.UseCases.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task<Producto> CreateAsync(Producto producto);
    Task UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
    Task<IEnumerable<Producto>> SearchByNameAsync(string nombre);
}