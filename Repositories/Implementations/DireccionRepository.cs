namespace DeliveryApi.Repositories.Implementations;

using DeliveryApi.Data;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class DireccionRepository : IDireccionRepository
{
    private readonly AppDbContext _context;

    public DireccionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Direccion>> GetAllAsync() =>
        await _context.Direcciones.ToListAsync();

    public async Task<Direccion?> GetByIdAsync(int id) =>
        await _context.Direcciones.FindAsync(id);

    public async Task AddAsync(Direccion entity) =>
        await _context.Direcciones.AddAsync(entity);

    public async Task UpdateAsync(Direccion entity) =>
        _context.Direcciones.Update(entity);

    public async Task DeleteAsync(int id)
    {
        var direccion = await GetByIdAsync(id);
        if (direccion != null)
            _context.Direcciones.Remove(direccion);
    }

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();

    public async Task<IEnumerable<Direccion>> GetByClienteIdAsync(int clienteId) =>
        await _context.Direcciones
            .Where(d => d.ClienteId == clienteId)
            .ToListAsync();
}