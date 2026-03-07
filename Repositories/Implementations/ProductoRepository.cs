namespace DeliveryApi.Repositories.Implementations;

using DeliveryApi.Data;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Producto entity)
    {
        await _context.Productos.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var producto = await GetByIdAsync(id);

        if (producto != null)
            _context.Productos.Remove(producto);
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        var productos = await _context.Productos.ToListAsync();
        return productos;
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        return producto;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Producto>> searchByNameAsync(string nombre)
    {
        return await _context.Productos
            .Where(producto => producto.Nombre.Contains(nombre))
            .ToListAsync();
    }

    public async Task UpdateAsync(Producto entity)
    {
        _context.Productos.Update(entity);   
    }
}