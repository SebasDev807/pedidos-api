namespace DeliveryApi.Domain.Interfaces;

using DeliveryApi.Domain.Entities;

public interface IProductoRepository : IGenericRepository<Producto>
{
    Task<IEnumerable<Producto>> searchByNameAsync(string nombre);
}
