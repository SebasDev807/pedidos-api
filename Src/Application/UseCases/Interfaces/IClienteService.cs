namespace DeliveryApi.Application.UseCases.Interfaces;

using DeliveryApi.Application.DTOs.Clientes;

public interface IClienteService
{
    Task<ClienteDto?> GetPerfilAsync(int usuarioId);
    Task<DireccionDto> AgregarDireccionAsync(int usuarioId, CrearDireccionDto dto);
    Task EliminarDireccionAsync(int usuarioId, int direccionId);
}