namespace DeliveryApi.Services.Interfaces;

using DeliveryApi.Models;

public interface IProductoService
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task<Producto> CreateAsync(Producto producto);
    Task UpdateAsync(Producto producto);
    Task DeleteAsync(int id);
    Task<IEnumerable<Producto>> SearchByNameAsync(string nombre);
}