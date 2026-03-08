namespace DeliveryApi.Controllers;

using DeliveryApi.DTOs.Clientes;
using DeliveryApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet("perfil")]
    public async Task<IActionResult> GetPerfil()
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var perfil = await _service.GetPerfilAsync(usuarioId);
        if (perfil == null) return NotFound();
        return Ok(perfil);
    }

    [HttpPost("direcciones")]
    public async Task<IActionResult> AgregarDireccion([FromBody] CrearDireccionDto dto)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var direccion = await _service.AgregarDireccionAsync(usuarioId, dto);
        return Created("", new { direccion.Id, direccion.Nombre, direccion.Direccion_ });
    }

    [HttpDelete("direcciones/{direccionId}")]
    public async Task<IActionResult> EliminarDireccion(int direccionId)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _service.EliminarDireccionAsync(usuarioId, direccionId);
        return NoContent();
    }
}