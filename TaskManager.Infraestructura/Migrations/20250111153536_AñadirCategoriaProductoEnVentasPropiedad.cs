using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class AñadirCategoriaProductoEnVentasPropiedad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaProductoEnVentas",
                table: "ProductosParaVender",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaProductoEnVentas",
                table: "ProductosParaVender");
        }
    }
}
