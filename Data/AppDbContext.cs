using DeliveryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApi.Data;

/*
* Maneja la comunicacion con la base de datos
*/
public class AppDbContext : DbContext
{

    // Inyeccion de dependencias, Asp construye el contexto automatrcamente
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /*
    * Cada DbSet<T> Representa una tabla en la Base De Datos
    */
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Direccion> Direcciones => Set<Direccion>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<EstadoPedido> EstadosPedido => Set<EstadoPedido>();

    //Este metodo se ejecuta una vez cuando EF Core construye el modelo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Email unico por usuario
        modelBuilder.Entity<Usuario>()
            .HasIndex(user => user.Email)
            .IsUnique();

        // Precision de decimales en SQLite
        modelBuilder.Entity<Producto>()
            .Property(producto => producto.Precio)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pedido>()
            .Property(pedido => pedido.Total)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<DetallePedido>()
            .Property(detallePedido => detallePedido.PrecioUnitario)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<DetallePedido>()
            .Property(detallePedido => detallePedido.Subtotal)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<EstadoPedido>().HasData(
            new EstadoPedido { Id = 1, Nombre = "PENDIENTE" },
            new EstadoPedido { Id = 2, Nombre = "CONFIRMADO" },
            new EstadoPedido { Id = 3, Nombre = "EN_PREPARACION" },
            new EstadoPedido { Id = 4, Nombre = "EN_CAMINO" },
            new EstadoPedido { Id = 5, Nombre = "ENTREGADO" },
            new EstadoPedido { Id = 6, Nombre = "CANCELADO" }

        );

    }

}