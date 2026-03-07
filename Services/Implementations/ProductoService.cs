namespace DeliveryApi.Services.Implementations;

using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using DeliveryApi.Services.Interfaces;

public class ProductoService : IProductoService
{   
    private readonly IProductoRepository _repo;

    public ProductoService(IProductoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Producto> CreateAsync(Producto producto)
    {
        await _repo.AddAsync(producto);
        await _repo.SaveAsync();
        return producto;
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Producto>> SearchByNameAsync(string nombre)
    {
       return await _repo.searchByNameAsync(nombre);
    }

    public async Task UpdateAsync(Producto producto)
    {
        await _repo.UpdateAsync(producto);
        await _repo.SaveAsync();
    }
}