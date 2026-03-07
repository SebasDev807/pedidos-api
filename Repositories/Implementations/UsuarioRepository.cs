namespace DeliveryApi.Repositories.Implementations;

using DeliveryApi.Data;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Usuario entity)
    {
        await _context.Usuarios.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await GetByIdAsync(id);

        if (usuario != null)
            _context.Usuarios.Remove(usuario);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(usuario => usuario.Email == email);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email);

    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario entity)
    {
        _context.Update(entity);
    }
}