using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class EdicionPedidosClientesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductosParaVenderId",
                table: "PedidosClientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductosParaVenderId",
                table: "PedidosClientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
