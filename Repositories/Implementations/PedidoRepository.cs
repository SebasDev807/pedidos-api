namespace DeliveryApi.Repositories.Implementations;

using DeliveryApi.Data;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class PedidoRepository : IPedidoRepository
{

    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Pedido entity)
    {
        await _context.Pedidos.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var pedido = await GetByIdAsync(id);

        if (pedido != null)
            _context.Pedidos.Remove(pedido);
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync() {
       return await _context.Pedidos
           .Include(p => p.EstadoPedido)
           .Include(p => p.Cliente)
           .Include(p => p.DetallesPedido)
               .ThenInclude(d => d.Producto)
           .ToListAsync();
    }
    public async Task<IEnumerable<Pedido>> GetByClienteAsync(int clienteId)
    {
        return await _context.Pedidos
            .Where(pedido => pedido.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<Pedido?> GetByIdAsync(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        return pedido;
    }

    public async Task<Pedido?> getByIdWithDetailsAsync(int id)
    {
        var pedido = await _context.Pedidos
        .Include(pedido => pedido.DetallesPedido)
            .ThenInclude(detalles => detalles.Producto)
            .Include(pedido => pedido.EstadoPedido)
            .Include(pedido => pedido.Cliente)
            .FirstOrDefaultAsync(pedido => pedido.Id == id);

        return pedido;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pedido entity)
    {
        _context.Pedidos.Update(entity);
    }
}