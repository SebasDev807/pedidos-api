namespace DeliveryApi.Repositories.Interfaces;

using DeliveryApi.Models;

public interface IProductoRepository : IGenericRepository<Producto>
{
    Task<IEnumerable<Producto>> searchByNameAsync(string nombre);
    
}