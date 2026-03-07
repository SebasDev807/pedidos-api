namespace DeliveryApi.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Telefono { get; set; }

    // FK a usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    // Un cliente tiene muchas direcciones y pedidos
    public ICollection<Direccion> Direcciones { get; set; } = [];
    public ICollection<Pedido> Pedidos { get; set; } = [];


}
