namespace DeliveryApi.Controllers;

using Dapper;
using DeliveryApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[ApiController]
[Route("api/[controller]")]
public class PruebaController : ControllerBase
{
    private readonly IDbConnection _db;

    public PruebaController(IDbConnection db)
    {
        _db = db;
    }

    // 1. Llama este primero
    [HttpPost("setup")]
    public async Task<IActionResult> Setup()
    {
        // Usuario
        var usuarioId = await _db.ExecuteScalarAsync<int>(@"
            INSERT INTO usuarios (nombre, email, contraseña, rol, telefono) 
            VALUES ('Sebastian', 'sebastian@test.com', '123456', 'cliente', '3001234567')
            RETURNING id");

        // Cliente
        var clienteId = await _db.ExecuteScalarAsync<int>(@"
            INSERT INTO clientes (nombre, telefono, idUsuario) 
            VALUES ('Sebastian', '3001234567', @UsuarioId)
            RETURNING id", new { UsuarioId = usuarioId });

        // Productos
        await _db.ExecuteAsync(@"
            INSERT INTO productos (nombre, descripcion, precio) VALUES
            ('Pizza Margarita', 'Pizza clasica', 29.99),
            ('Hamburguesa', 'Con papas fritas', 19.99),
            ('Gaseosa', '500ml', 5.99)");

        return Ok(new { usuarioId, clienteId });
    }

    // 2. Verifica que todo existe
    [HttpGet("resumen")]
    public async Task<IActionResult> Resumen()
    {
        return Ok(new
        {
            usuarios = await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM usuarios"),
            clientes = await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM clientes"),
            productos = await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM productos"),
            estados = await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM estadosPedido"),
            pedidos = await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM pedidos")
        });
    }

    // 3. Limpia todo si quieres empezar de cero
    [HttpDelete("limpiar")]
    public async Task<IActionResult> Limpiar()
    {
        await _db.ExecuteAsync("DELETE FROM detallePedido");
        await _db.ExecuteAsync("DELETE FROM pedidos");
        await _db.ExecuteAsync("DELETE FROM productos");
        await _db.ExecuteAsync("DELETE FROM clientes");
        await _db.ExecuteAsync("DELETE FROM usuarios");
        return Ok("Base de datos limpia");
    }
}