namespace DeliveryApi.Controllers;

using System.Security.Claims;
using DeliveryApi.DTOs.Pedidos;
using DeliveryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _service;

    public PedidosController(IPedidoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _service.GetAllAsync();

        var response = pedidos.Select(pedido => new PedidoResponseDto
        {
            Id = pedido.Id,
            IdDireccionEntrega = pedido.IdDireccionEntrega,
            Total = pedido.ValorTotal,
            Estado = pedido.EstadoPedido?.Nombre ?? "Desconocido",
            FechaCreacion = pedido.FechaCreacion,
            FechaEntrega = pedido.FechaEntrega,
            Detalles = pedido.DetallesPedido.Select(d => new DetalleResponseDto
            {
                Producto = d.Producto?.Nombre ?? "Desconocido",
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList()
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await _service.GetByIdAsync(id);
        if (pedido == null) return NotFound($"Pedido con id {id} no existe");

        var response = new PedidoResponseDto
        {
            Id = pedido.Id,
            IdDireccionEntrega = pedido.IdDireccionEntrega,
            Total = pedido.ValorTotal,
            Estado = pedido.EstadoPedido?.Nombre ?? "DESCONOCIDO",
            FechaCreacion = pedido.FechaCreacion,
            FechaEntrega = pedido.FechaEntrega,
            Detalles = pedido.DetallesPedido.Select(detalle => new DetalleResponseDto
            {
                Producto = detalle.Producto?.Nombre ?? "DESCONOCIDO",
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario,
                Subtotal = detalle.Subtotal
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearPedidoDto crearPedidoDto)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var items = crearPedidoDto.Items
            .Select(item => (item.ProductoId, item.Cantidad))
            .ToList();

        var pedido = await _service.CreateAsync(
            crearPedidoDto.ClienteId,
            usuarioId,
            crearPedidoDto.IdDireccionEntrega,
            items
        );

        return Created("", new
        {
            id = pedido.Id,
            clienteId = pedido.IdCliente,
            idDireccionEntrega = pedido.IdDireccionEntrega,
            total = pedido.ValorTotal,
            estadoId = pedido.IdEstado,
            fechaCreacion = pedido.FechaCreacion
        });
    }

    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] ActualizarEstadoDto dto)
    {
        await _service.UpdateEstadoAsync(id, dto.EstadoId);
        return NoContent();
    }
}