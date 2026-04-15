namespace DeliveryApi.Application.UseCases.Implementations;

using DeliveryApi.Application.DTOs.Clientes;
using DeliveryApi.Exceptions;
using DeliveryApi.Domain.Entities;
using DeliveryApi.Domain.Interfaces;
using DeliveryApi.Application.UseCases.Interfaces;

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

    public async Task<DireccionDto> AgregarDireccionAsync(int usuarioId, CrearDireccionDto dto)
    {
        var cliente = await _clienteRepo.GetByUsuarioIdAsync(usuarioId)
            ?? throw new Exception("Cliente no encontrado");

        var direccion = new Direccion
        {
            Nombre = dto.Nombre,
            Direccion_ = dto.Direccion_,
            ClienteId = cliente.Id
        };

        await _direccionRepo.AddAsync(direccion);
        await _direccionRepo.SaveAsync();

        return new DireccionDto
        {
            Id = direccion.Id,
            Nombre = direccion.Nombre,
            Direccion_ = direccion.Direccion_
        };
    }

    public async Task EliminarDireccionAsync(int usuarioId, int direccionId)
    {
        var cliente = await _clienteRepo.GetByUsuarioIdAsync(usuarioId)
            ?? throw new Exception("Cliente no encontrado");

        var direccion = await _direccionRepo.GetByIdAsync(direccionId)
            ?? throw new Exception("Dirección no encontrada");

        // Verifica que la dirección pertenece al cliente
        if (direccion.ClienteId != cliente.Id)
            throw new ConflictException("No tienes permiso para eliminar esta dirección");

        await _direccionRepo.DeleteAsync(direccionId);
        await _direccionRepo.SaveAsync();
    }
}