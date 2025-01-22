using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class EdicionPedidosClientesModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPedido",
                table: "PedidosClientes");

            migrationBuilder.AddColumn<double>(
                name: "PrecioTotal",
                table: "PedidosClientes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioTotal",
                table: "PedidosClientes");

            migrationBuilder.AddColumn<int>(
                name: "CantidadPedido",
                table: "PedidosClientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
