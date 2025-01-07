using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CrearEntidadOrdenesProductosSuministradores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrdenProductoSuministradorModelId",
                table: "ProductosSuministradores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrdenesProductosSuministradores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductoSuministradorId = table.Column<int>(type: "int", nullable: false),
                    CantidadDeOrden = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraDeCreacion = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesProductosSuministradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesProductosSuministradores_ProductosSuministradores_ProductoSuministradorId",
                        column: x => x.ProductoSuministradorId,
                        principalTable: "ProductosSuministradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosSuministradores_OrdenProductoSuministradorModelId",
                table: "ProductosSuministradores",
                column: "OrdenProductoSuministradorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesProductosSuministradores_ProductoSuministradorId",
                table: "OrdenesProductosSuministradores",
                column: "ProductoSuministradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosSuministradores_OrdenesProductosSuministradores_OrdenProductoSuministradorModelId",
                table: "ProductosSuministradores",
                column: "OrdenProductoSuministradorModelId",
                principalTable: "OrdenesProductosSuministradores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosSuministradores_OrdenesProductosSuministradores_OrdenProductoSuministradorModelId",
                table: "ProductosSuministradores");

            migrationBuilder.DropTable(
                name: "OrdenesProductosSuministradores");

            migrationBuilder.DropIndex(
                name: "IX_ProductosSuministradores_OrdenProductoSuministradorModelId",
                table: "ProductosSuministradores");

            migrationBuilder.DropColumn(
                name: "OrdenProductoSuministradorModelId",
                table: "ProductosSuministradores");
        }
    }
}
