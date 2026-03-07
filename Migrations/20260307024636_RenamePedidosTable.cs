using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApi.Migrations
{
    /// <inheritdoc />
    public partial class RenamePedidosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Pedido_PedidoId",
                table: "DetallesPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Clientes_ClienteId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_EstadosPedido_EstadoPedidoId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Usuarios_UsuarioId",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "Pedidos");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_UsuarioId",
                table: "Pedidos",
                newName: "IX_Pedidos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_EstadoPedidoId",
                table: "Pedidos",
                newName: "IX_Pedidos_EstadoPedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Pedidos_PedidoId",
                table: "DetallesPedido",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_EstadosPedido_EstadoPedidoId",
                table: "Pedidos",
                column: "EstadoPedidoId",
                principalTable: "EstadosPedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Pedidos_PedidoId",
                table: "DetallesPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_EstadosPedido_EstadoPedidoId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Pedido");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedido",
                newName: "IX_Pedido_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_EstadoPedidoId",
                table: "Pedido",
                newName: "IX_Pedido_EstadoPedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedido",
                newName: "IX_Pedido_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Pedido_PedidoId",
                table: "DetallesPedido",
                column: "PedidoId",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Clientes_ClienteId",
                table: "Pedido",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_EstadosPedido_EstadoPedidoId",
                table: "Pedido",
                column: "EstadoPedidoId",
                principalTable: "EstadosPedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Usuarios_UsuarioId",
                table: "Pedido",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
