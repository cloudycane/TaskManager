using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CrearPedidoProductoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoProducto",
                columns: table => new
                {
                    PedidoClienteId = table.Column<int>(type: "int", nullable: false),
                    ProductoParaVenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProducto", x => new { x.PedidoClienteId, x.ProductoParaVenderId });
                    table.ForeignKey(
                        name: "FK_PedidoProducto_PedidosClientes_PedidoClienteId",
                        column: x => x.PedidoClienteId,
                        principalTable: "PedidosClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProducto_ProductosParaVender_ProductoParaVenderId",
                        column: x => x.ProductoParaVenderId,
                        principalTable: "ProductosParaVender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProducto_ProductoParaVenderId",
                table: "PedidoProducto",
                column: "ProductoParaVenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProducto");
        }
    }
}
