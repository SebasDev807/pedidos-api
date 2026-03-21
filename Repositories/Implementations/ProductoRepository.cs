namespace DeliveryApi.Repositories.Implementations;

using Dapper;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using System.Data;

public class ProductoRepository : IProductoRepository
{
    private readonly IDbConnection _db;

    public ProductoRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <summary>
    /// Inserta un nuevo producto en la base de datos
    /// </summary>
    /// <param name="entity">El producto a insertar</param>
    public async Task AddAsync(Producto entity) =>
        await _db.ExecuteAsync(
            "INSERT INTO productos (nombre, descripcion, precio) VALUES (@Nombre, @Descripcion, @Precio)", entity);

    /// <summary>
    /// Elimina un producto por su ID
    /// </summary>
    /// <param name="id">ID del producto a eliminar</param>
    public async Task DeleteAsync(int id) =>
        await _db.ExecuteAsync(
            "DELETE FROM productos WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Obtiene todos los productos
    /// </summary>
    /// <returns>Lista de todos los productos</returns>
    public async Task<IEnumerable<Producto>> GetAllAsync() =>
        await _db.QueryAsync<Producto>("SELECT * FROM productos");

    /// <summary>
    /// Obtiene un producto por su ID
    /// </summary>
    /// <param name="id">ID del producto</param>
    /// <returns>El producto encontrado o null</returns>
    public async Task<Producto?> GetByIdAsync(int id) =>
        await _db.QueryFirstOrDefaultAsync<Producto>(
            "SELECT * FROM productos WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Guarda los cambios pendientes. No requerido en Dapper ya que cada operacion es inmediata
    /// </summary>
    public async Task SaveAsync() =>
        await Task.CompletedTask;

    /// <summary>
    /// Busca productos cuyo nombre contenga el texto indicado
    /// </summary>
    /// <param name="nombre">Texto a buscar en el nombre del producto</param>
    /// <returns>Lista de productos que coinciden con la busqueda</returns>
    public async Task<IEnumerable<Producto>> searchByNameAsync(string nombre) =>
        await _db.QueryAsync<Producto>(
            "SELECT * FROM productos WHERE nombre LIKE @Nombre", new { Nombre = $"%{nombre}%" });

    /// <summary>
    /// Actualiza un producto existente en la base de datos
    /// </summary>
    /// <param name="entity">El producto con los datos actualizados</param>
    public async Task UpdateAsync(Producto entity) =>
        await _db.ExecuteAsync(
            "UPDATE productos SET nombre = @Nombre, descripcion = @Descripcion, precio = @Precio WHERE id = @Id", entity);
}