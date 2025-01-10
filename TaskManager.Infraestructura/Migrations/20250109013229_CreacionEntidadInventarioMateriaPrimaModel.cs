using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CreacionEntidadInventarioMateriaPrimaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventarioMateriaPrimas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraProductoSuministradorId = table.Column<int>(type: "int", nullable: false),
                    CategoriaProductoInventario = table.Column<int>(type: "int", nullable: false),
                    FechaDistribucion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoProductoInventario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioMateriaPrimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioMateriaPrimas_CompraProductoSuministradorFacturacion_CompraProductoSuministradorId",
                        column: x => x.CompraProductoSuministradorId,
                        principalTable: "CompraProductoSuministradorFacturacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMateriaPrimas_CompraProductoSuministradorId",
                table: "InventarioMateriaPrimas",
                column: "CompraProductoSuministradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventarioMateriaPrimas");
        }
    }
}
