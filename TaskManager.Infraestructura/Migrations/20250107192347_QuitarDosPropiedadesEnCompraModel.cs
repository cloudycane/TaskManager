using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class QuitarDosPropiedadesEnCompraModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoRepresentante",
                table: "CompraProductoSuministradorFacturacion");

            migrationBuilder.DropColumn(
                name: "NombreRepresentante",
                table: "CompraProductoSuministradorFacturacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApellidoRepresentante",
                table: "CompraProductoSuministradorFacturacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreRepresentante",
                table: "CompraProductoSuministradorFacturacion",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
