namespace DeliveryApi.Repositories.Implementations;

using Dapper;
using DeliveryApi.Models;
using DeliveryApi.Repositories.Interfaces;
using System.Data;

public class PedidoRepository : IPedidoRepository
{
    private readonly IDbConnection _db;

    public PedidoRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <summary>
    /// Inserta un nuevo pedido en la base de datos
    /// </summary>
    /// <param name="entity">El pedido a insertar</param>
    public async Task AddAsync(Pedido entity)
    {
        var sql = @"
        INSERT INTO pedidos (idCliente, idUsuario, idEstado, valorTotal, idDireccionEntrega, fechaCreacion, fechaEntrega)
        VALUES (@IdCliente, @IdUsuario, @IdEstado, @ValorTotal, @IdDireccionEntrega, @FechaCreacion, @FechaEntrega)
        RETURNING id";
        var id = await _db.ExecuteScalarAsync<int>(sql, entity);
        entity.Id = id;
    
    }

    /// <summary>
    /// Elimina un pedido por su ID
    /// </summary>
    /// <param name="id">ID del pedido a eliminar</param>
    public async Task DeleteAsync(int id) =>
        await _db.ExecuteAsync("DELETE FROM pedidos WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Obtiene todos los pedidos incluyendo cliente, estado y detalles con productos
    /// </summary>
    /// <returns>Lista de todos los pedidos con sus relaciones</returns>
    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        var sql = @"
            SELECT 
                p.*, 
                c.*, 
                e.*,
                d.*,
                pr.*
            FROM pedidos p
            LEFT JOIN clientes c ON p.idCliente = c.id
            LEFT JOIN estadosPedido e ON p.idEstado = e.id
            LEFT JOIN detallePedido d ON d.idPedido = p.id
            LEFT JOIN productos pr ON d.idProducto = pr.id";

        var pedidosDict = new Dictionary<int, Pedido>();

        await _db.QueryAsync<Pedido, Cliente, EstadoPedido, DetallePedido, Producto, Pedido>(
            sql,
            (pedido, cliente, estado, detalle, producto) =>
            {
                if (!pedidosDict.TryGetValue(pedido.Id, out var pedidoExistente))
                {
                    pedidoExistente = pedido;
                    pedidoExistente.Cliente = cliente;
                    pedidoExistente.EstadoPedido = estado;
                    pedidoExistente.DetallesPedido = [];
                    pedidosDict[pedido.Id] = pedidoExistente;
                }

                if (detalle != null)
                {
                    detalle.Producto = producto;
                    pedidoExistente.DetallesPedido.Add(detalle);
                }

                return pedidoExistente;
            },
            splitOn: "id,id,id,id"
        );

        return pedidosDict.Values;
    }

    /// <summary>
    /// Obtiene todos los pedidos de un cliente específico
    /// </summary>
    /// <param name="clienteId">ID del cliente</param>
    /// <returns>Lista de pedidos del cliente</returns>
    public async Task<IEnumerable<Pedido>> GetByClienteAsync(int clienteId) =>
        await _db.QueryAsync<Pedido>(
            "SELECT * FROM pedidos WHERE idCliente = @ClienteId", new { ClienteId = clienteId });

    /// <summary>
    /// Obtiene un pedido por su ID sin relaciones
    /// </summary>
    /// <param name="id">ID del pedido</param>
    /// <returns>El pedido encontrado o null</returns>
    public async Task<Pedido?> GetByIdAsync(int id) =>
        await _db.QueryFirstOrDefaultAsync<Pedido>(
            "SELECT * FROM pedidos WHERE id = @Id", new { Id = id });

    /// <summary>
    /// Obtiene un pedido por su ID incluyendo detalles, productos, estado y cliente
    /// </summary>
    /// <param name="id">ID del pedido</param>
    /// <returns>El pedido con todas sus relaciones o null</returns>
    public async Task<Pedido?> getByIdWithDetailsAsync(int id)
    {
        var sql = @"
            SELECT 
                p.*, 
                c.*, 
                e.*,
                d.*,
                pr.*
            FROM pedidos p
            LEFT JOIN clientes c ON p.idCliente = c.id
            LEFT JOIN estadosPedido e ON p.idEstado = e.id
            LEFT JOIN detallePedido d ON d.idPedido = p.id
            LEFT JOIN productos pr ON d.idProducto = pr.id
            WHERE p.id = @Id";

        var pedidosDict = new Dictionary<int, Pedido>();

        await _db.QueryAsync<Pedido, Cliente, EstadoPedido, DetallePedido, Producto, Pedido>(
            sql,
            (pedido, cliente, estado, detalle, producto) =>
            {
                if (!pedidosDict.TryGetValue(pedido.Id, out var pedidoExistente))
                {
                    pedidoExistente = pedido;
                    pedidoExistente.Cliente = cliente;
                    pedidoExistente.EstadoPedido = estado;
                    pedidoExistente.DetallesPedido = [];
                    pedidosDict[pedido.Id] = pedidoExistente;
                }

                if (detalle != null)
                {
                    detalle.Producto = producto;
                    pedidoExistente.DetallesPedido.Add(detalle);
                }

                return pedidoExistente;
            },
            splitOn: "id,id,id,id",
            param: new { Id = id }
        );

        return pedidosDict.Values.FirstOrDefault();
    }

    /// <summary>
    /// Guarda los cambios pendientes. No requerido en Dapper ya que cada operacion es inmediata
    /// </summary>
    public async Task SaveAsync() =>
        await Task.CompletedTask;

    /// <summary>
    /// Actualiza un pedido existente en la base de datos
    /// </summary>
    /// <param name="entity">El pedido con los datos actualizados</param>
    public async Task UpdateAsync(Pedido entity)
    {
        var sql = @"
        UPDATE pedidos 
        SET idCliente = @IdCliente,
            idUsuario = @IdUsuario,
            idEstado = @IdEstado,
            valorTotal = @ValorTotal,
            idDireccionEntrega = @IdDireccionEntrega,
            fechaEntrega = @FechaEntrega
        WHERE id = @Id";
        await _db.ExecuteAsync(sql, entity);
    }
}