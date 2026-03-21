namespace DeliveryApi.Repositories.Implementations;

using Dapper;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using System.Data;

public class DireccionRepository : IDireccionRepository
{
    private readonly IDbConnection _db;

    public DireccionRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <summary>
    /// Obtiene todas las direcciones de la base de datos
    /// </summary>
    /// <returns>Lista de todas las direcciones</returns>
    public async Task<IEnumerable<Direccion>> GetAllAsync() =>
        await _db.QueryAsync<Direccion>("SELECT * FROM direcciones");

    /// <summary>
    /// Obtiene una direccion por su ID
    /// </summary>
    /// <param name="id">ID de la direccion</param>
    /// <returns>La direccion encontrada o null</returns>
    public async Task<Direccion?> GetByIdAsync(int id) =>
        await _db.QueryFirstOrDefaultAsync<Direccion>(
            "SELECT * FROM direcciones WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Inserta una nueva direccion en la base de datos
    /// </summary>
    /// <param name="entity">La direccion a insertar</param>
    public async Task AddAsync(Direccion entity) =>
        await _db.ExecuteAsync(
        "INSERT INTO direcciones (idCliente, nombre, direcccion) VALUES (@ClienteId, @Nombre, @Direccion_)", entity);

    /// <summary>
    /// Actualiza una direccion existente en la base de datos
    /// </summary>
    /// <param name="entity">La direccion con los datos actualizados</param>
    public async Task UpdateAsync(Direccion entity) =>
        await _db.ExecuteAsync(
        "UPDATE direcciones SET nombre = @Nombre, direcccion = @Direccion_ WHERE id = @Id", entity);

    /// <summary>
    /// Elimina una direccion por su ID
    /// </summary>
    /// <param name="id">ID de la direccion a eliminar</param>
    public async Task DeleteAsync(int id) =>
        await _db.ExecuteAsync(
            "DELETE FROM direcciones WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Guarda los cambios pendientes. No requerido en Dapper ya que cada operacion es inmediata
    /// </summary>
    public async Task SaveAsync() =>
        await Task.CompletedTask;

    /// <summary>
    /// Obtiene todas las direcciones asociadas a un cliente especifico
    /// </summary>
    /// <param name="clienteId">ID del cliente</param>
    /// <returns>Lista de direcciones del cliente</returns>
    public async Task<IEnumerable<Direccion>> GetByClienteIdAsync(int clienteId) =>
        await _db.QueryAsync<Direccion>(
            "SELECT * FROM direcciones WHERE idCliente = @ClienteId", new { ClienteId = clienteId });
}