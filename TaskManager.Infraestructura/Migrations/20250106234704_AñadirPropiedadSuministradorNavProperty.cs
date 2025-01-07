using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class AñadirPropiedadSuministradorNavProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductosSuministradores_SuministradorId",
                table: "ProductosSuministradores",
                column: "SuministradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosSuministradores_Suministradores_SuministradorId",
                table: "ProductosSuministradores",
                column: "SuministradorId",
                principalTable: "Suministradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosSuministradores_Suministradores_SuministradorId",
                table: "ProductosSuministradores");

            migrationBuilder.DropIndex(
                name: "IX_ProductosSuministradores_SuministradorId",
                table: "ProductosSuministradores");
        }
    }
}
