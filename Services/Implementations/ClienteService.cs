namespace DeliveryApi.Services.Implementations;

using DeliveryApi.DTOs.Clientes;
using DeliveryApi.Exceptions;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using DeliveryApi.Services.Interfaces;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepo;
    private readonly IDireccionRepository _direccionRepo;

    public ClienteService(IClienteRepository clienteRepo, IDireccionRepository direccionRepo)
    {
        _clienteRepo = clienteRepo;
        _direccionRepo = direccionRepo;
    }

    public async Task<ClienteDto?> GetPerfilAsync(int usuarioId)
    {
        var cliente = await _clienteRepo.GetByUsuarioIdAsync(usuarioId);
        if (cliente == null) return null;

        return new ClienteDto
        {
            Id = cliente.Id,
            Nombre = cliente.Nombre,
            Telefono = cliente.Telefono,
            Direcciones = cliente.Direcciones.Select(d => new DireccionDto
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Direccion_ = d.Direccion_
            }).ToList()
        };
    }

    public async Task<Direccion> AgregarDireccionAsync(int usuarioId, CrearDireccionDto dto)
    {
        var cliente = await _clienteRepo.GetByUsuarioIdAsync(usuarioId)
            ?? throw new KeyNotFoundException("Cliente no encontrado");

        var direccion = new Direccion
        {
            Nombre = dto.Nombre,
            Direccion_ = dto.Direccion_,
            ClienteId = cliente.Id
        };

        await _direccionRepo.AddAsync(direccion);
        await _direccionRepo.SaveAsync();

        return direccion;
    }

    public async Task EliminarDireccionAsync(int usuarioId, int direccionId)
    {
        var cliente = await _clienteRepo.GetByUsuarioIdAsync(usuarioId)
            ?? throw new KeyNotFoundException("Cliente no encontrado");

        var direccion = await _direccionRepo.GetByIdAsync(direccionId)
            ?? throw new KeyNotFoundException("Dirección no encontrada");

        if (direccion.ClienteId != cliente.Id)
            throw new UnauthorizedAccessException("No tienes permiso para eliminar esta dirección");

        await _direccionRepo.DeleteAsync(direccionId);
        await _direccionRepo.SaveAsync();
    }
}