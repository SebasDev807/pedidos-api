namespace DeliveryApi.Repositories.Implementations;

using DeliveryApi.Data;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync() =>
        await _context.Clientes.ToListAsync();

    public async Task<Cliente?> GetByIdAsync(int id) =>
        await _context.Clientes.FindAsync(id);

    public async Task AddAsync(Cliente entity) =>
        await _context.Clientes.AddAsync(entity);

    public async Task UpdateAsync(Cliente entity) =>
        _context.Clientes.Update(entity);

    public async Task DeleteAsync(int id)
    {
        var cliente = await GetByIdAsync(id);
        if (cliente != null)
            _context.Clientes.Remove(cliente);
    }

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();

    public async Task<Cliente?> GetByUsuarioIdAsync(int usuarioId) =>
        await _context.Clientes
            .Include(c => c.Direcciones)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
}