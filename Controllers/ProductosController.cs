namespace DeliveryApi.Controllers;

using DeliveryApi.DTOs.Productos;
using DeliveryApi.Models;
using DeliveryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductosController(IProductoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _service.GetAllAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _service.GetByIdAsync(id);
        if (producto == null) return NotFound($"Producto con id {id} no existe");

        return Ok(producto);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Search([FromQuery] string nombre)
    {
        var productos = await _service.SearchByNameAsync(nombre);
        return Ok(productos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearProductoDto crearProductoDto)
    {
        var producto = new Producto
        {
            Nombre = crearProductoDto.Nombre,
            Descripcion = crearProductoDto.Descripcion,
            Precio = crearProductoDto.Precio
        };

        var creado = await _service.CreateAsync(producto);
        return Created("", creado);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CrearProductoDto actualizarProductoDto)
    {
        var producto = await _service.GetByIdAsync(id);
        if (producto == null) return NotFound($"Producto con id {id} no existe");

        producto.Nombre = actualizarProductoDto.Nombre;
        producto.Descripcion = actualizarProductoDto.Descripcion;
        producto.Precio = actualizarProductoDto.Precio;

        await _service.UpdateAsync(producto);
        return Ok(producto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _service.GetByIdAsync(id);
        if(producto == null) return NotFound($"Producto con id {id} no existe");

        await _service.DeleteAsync(id);
        return NoContent();
    }


}
