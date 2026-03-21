namespace DeliveryApi.Repositories.Implementations;

using Dapper;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using System.Data;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _db;

    public UsuarioRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <summary>
    /// Inserta un nuevo usuario en la base de datos
    /// </summary>
    /// <param name="entity">El usuario a insertar</param>
    public async Task AddAsync(Usuario entity)
    {
        var id = await _db.ExecuteScalarAsync<int>(
            "INSERT INTO usuarios (nombre, email, password, rol, telefono) VALUES (@Nombre, @Email, @Password, @Rol, @Telefono) RETURNING id", entity);
        entity.Id = id;
    }

    /// <summary>
    /// Elimina un usuario por su ID
    /// </summary>
    /// <param name="id">ID del usuario a eliminar</param>
    public async Task DeleteAsync(int id) =>
        await _db.ExecuteAsync(
            "DELETE FROM usuarios WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Verifica si un email ya existe en la base de datos
    /// </summary>
    /// <param name="email">Email a verificar</param>
    /// <returns>True si el email existe, false en caso contrario</returns>
    public async Task<bool> EmailExistsAsync(string email)
    {
        var result = await _db.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM usuarios WHERE email = @Email", new { Email = email });
        return result > 0;
    }

    /// <summary>
    /// Obtiene todos los usuarios
    /// </summary>
    /// <returns>Lista de todos los usuarios</returns>
    public async Task<IEnumerable<Usuario>> GetAllAsync() =>
        await _db.QueryAsync<Usuario>("SELECT * FROM usuarios");

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    /// <param name="email">Email del usuario</param>
    /// <returns>El usuario encontrado o null</returns>
    public async Task<Usuario?> GetByEmailAsync(string email) =>
        await _db.QueryFirstOrDefaultAsync<Usuario>(
            "SELECT * FROM usuarios WHERE email = @Email", new { Email = email });

    /// <summary>
    /// Obtiene un usuario por su ID
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <returns>El usuario encontrado o null</returns>
    public async Task<Usuario?> GetByIdAsync(int id) =>
        await _db.QueryFirstOrDefaultAsync<Usuario>(
            "SELECT * FROM usuarios WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Guarda los cambios pendientes. No requerido en Dapper ya que cada operacion es inmediata
    /// </summary>
    public async Task SaveAsync() =>
        await Task.CompletedTask;

    /// <summary>
    /// Actualiza un usuario existente en la base de datos
    /// </summary>
    /// <param name="entity">El usuario con los datos actualizados</param>
    public async Task UpdateAsync(Usuario entity) =>
        await _db.ExecuteAsync(
            "UPDATE usuarios SET nombre = @Nombre, email = @Email, password = @Password, rol = @Rol WHERE id = @Id", entity);
}