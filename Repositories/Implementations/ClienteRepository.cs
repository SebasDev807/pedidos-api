namespace DeliveryApi.Repositories.Implementations;

using Dapper;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using System.Data;

public class ClienteRepository : IClienteRepository
{
    private readonly IDbConnection _db;

    public ClienteRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <summary>
    /// Obtiene todos los clientes de la base de datos
    /// </summary>
    /// <returns>Lista de todos los clientes</returns>
    public async Task<IEnumerable<Cliente>> GetAllAsync() =>
        await _db.QueryAsync<Cliente>("SELECT * FROM clientes");

    /// <summary>
    /// Obtiene un cliente por su ID
    /// </summary>
    /// <param name="id">ID del cliente</param>
    /// <returns>El cliente encontrado o null</returns>
    public async Task<Cliente?> GetByIdAsync(int id) =>
        await _db.QueryFirstOrDefaultAsync<Cliente>(
            "SELECT * FROM clientes WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Inserta un nuevo cliente en la base de datos
    /// </summary>
    /// <param name="entity">El cliente a insertar</param>
    public async Task AddAsync(Cliente entity)
    {
        var id = await _db.ExecuteScalarAsync<int>(
            "INSERT INTO clientes (nombre, telefono, idUsuario) VALUES (@Nombre, @Telefono, @UsuarioId) RETURNING id", entity);
        entity.Id = id;
    }

    /// <summary>
    /// Actualiza un cliente existente en la base de datos
    /// </summary>
    /// <param name="entity">El cliente con los datos actualizados</param>
    public async Task UpdateAsync(Cliente entity) =>
        await _db.ExecuteAsync(
            "UPDATE clientes SET nombre = @Nombre, telefono = @Telefono WHERE id = @Id", entity);

    /// <summary>
    /// Elimina un cliente por su ID
    /// </summary>
    /// <param name="id">ID del cliente a eliminar</param>
    public async Task DeleteAsync(int id) =>
        await _db.ExecuteAsync(
            "DELETE FROM clientes WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Guarda los cambios pendientes. No requerido en Dapper ya que cada operacion es inmediata
    /// </summary>
    public async Task SaveAsync() =>
        await Task.CompletedTask;

    /// <summary>
    /// Obtiene un cliente por el ID de su usuario asociado
    /// </summary>
    /// <param name="usuarioId">ID del usuario asociado al cliente</param>
    /// <returns>El cliente encontrado o null</returns>
    public async Task<Cliente?> GetByUsuarioIdAsync(int usuarioId) =>
    await _db.QueryFirstOrDefaultAsync<Cliente>(
        "SELECT * FROM clientes WHERE idUsuario = @UsuarioId", new { UsuarioId = usuarioId });
}