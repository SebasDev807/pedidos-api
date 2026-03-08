namespace DeliveryApi.Services.Interfaces;

using DeliveryApi.DTOs.Clientes;
using DeliveryApi.Models;

public interface IClienteService
{
    Task<ClienteDto?> GetPerfilAsync(int usuarioId);
    Task<Direccion> AgregarDireccionAsync(int usuarioId, CrearDireccionDto dto);
    Task EliminarDireccionAsync(int usuarioId, int direccionId);
}