using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CreacionEntidadPedidosClienteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidosClienteModelId",
                table: "ProductosParaVender",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PedidosClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductosParaVenderId = table.Column<int>(type: "int", nullable: false),
                    CantidadPedido = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<double>(type: "float", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreElectronicoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetodoDePago = table.Column<int>(type: "int", nullable: false),
                    FechaDePedido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosClientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosParaVender_PedidosClienteModelId",
                table: "ProductosParaVender",
                column: "PedidosClienteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosParaVender_PedidosClientes_PedidosClienteModelId",
                table: "ProductosParaVender",
                column: "PedidosClienteModelId",
                principalTable: "PedidosClientes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosParaVender_PedidosClientes_PedidosClienteModelId",
                table: "ProductosParaVender");

            migrationBuilder.DropTable(
                name: "PedidosClientes");

            migrationBuilder.DropIndex(
                name: "IX_ProductosParaVender_PedidosClienteModelId",
                table: "ProductosParaVender");

            migrationBuilder.DropColumn(
                name: "PedidosClienteModelId",
                table: "ProductosParaVender");
        }
    }
}
