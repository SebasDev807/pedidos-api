namespace DeliveryApi.Controllers;

using DeliveryApi.Data;
using DeliveryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PruebaController : ControllerBase
{
    private readonly AppDbContext _context;

    public PruebaController(AppDbContext context)
    {
        _context = context;
    }

    // 1. Llama este primero
    [HttpPost("setup")]
    public async Task<IActionResult> Setup()
    {
        // Usuario
        var usuario = new Usuario
        {
            Nombre = "Sebastian",
            Email = "sebastian@test.com",
            Password = "123456",
            Rol = "cliente"
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Cliente
        var cliente = new Cliente
        {
            Nombre = "Sebastian",
            Telefono = "3001234567",
            UsuarioId = usuario.Id
        };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Productos
        var productos = new List<Producto>
        {
            new Producto { Nombre = "Pizza Margarita", Descripcion = "Pizza clasica", Precio = 29.99m },
            new Producto { Nombre = "Hamburguesa", Descripcion = "Con papas fritas", Precio = 19.99m },
            new Producto { Nombre = "Gaseosa", Descripcion = "500ml", Precio = 5.99m }
        };
        _context.Productos.AddRange(productos);
        await _context.SaveChangesAsync();

        return Ok(new { usuario, cliente, productos });
    }

    // 2. Verifica que todo existe
    [HttpGet("resumen")]
    public async Task<IActionResult> Resumen()
    {
        return Ok(new
        {
            usuarios = await _context.Usuarios.CountAsync(),
            clientes = await _context.Clientes.CountAsync(),
            productos = await _context.Productos.CountAsync(),
            estados = await _context.EstadosPedido.CountAsync(),
            pedidos = await _context.Pedidos.CountAsync()
        });
    }

    // 3. Limpia todo si quieres empezar de cero
    [HttpDelete("limpiar")]
    public async Task<IActionResult> Limpiar()
    {
        _context.Pedidos.RemoveRange(_context.Pedidos);
        _context.Productos.RemoveRange(_context.Productos);
        _context.Clientes.RemoveRange(_context.Clientes);
        _context.Usuarios.RemoveRange(_context.Usuarios);
        await _context.SaveChangesAsync();
        return Ok("Base de datos limpia");
    }
}
