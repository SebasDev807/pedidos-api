namespace DeliveryApi.Controllers;

using DeliveryApi.DTOs.Pedidos;
using DeliveryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await _service.GetByIdAsync(id);
        if (pedido == null) return NotFound($"Pedido con id {id} no existe");

        //Mapear DTO de respuesta
        var response = new PedidoResponseDto
        {

            Id = pedido.Id,
            DireccionEntrega = pedido.DireccionEntrega,
            Total = pedido.Total,
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
        var items = crearPedidoDto.Items
            .Select(item => (item.ProductoId, item.Cantidad))
            .ToList();

        var pedido = await _service.CreateAsync(
            crearPedidoDto.ClienteId,
            1, //Hardcodeado por ahora
            crearPedidoDto.DireccionEntrega,
            items
        );

        return Created("", new
        {
            id = pedido.Id,
            clienteId = pedido.ClienteId,
            direccionEntrega = pedido.DireccionEntrega,
            total = pedido.Total,
            estadoId = pedido.EstadoPedidoId,
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