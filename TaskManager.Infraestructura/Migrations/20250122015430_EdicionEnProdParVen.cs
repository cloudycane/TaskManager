using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class EdicionEnProdParVen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosSuministradores_ProductosParaVender_ProductosParaVenderModelId",
                table: "ProductosSuministradores");

            migrationBuilder.DropIndex(
                name: "IX_ProductosSuministradores_ProductosParaVenderModelId",
                table: "ProductosSuministradores");

            migrationBuilder.DropColumn(
                name: "ProductosParaVenderModelId",
                table: "ProductosSuministradores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductosParaVenderModelId",
                table: "ProductosSuministradores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosSuministradores_ProductosParaVenderModelId",
                table: "ProductosSuministradores",
                column: "ProductosParaVenderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosSuministradores_ProductosParaVender_ProductosParaVenderModelId",
                table: "ProductosSuministradores",
                column: "ProductosParaVenderModelId",
                principalTable: "ProductosParaVender",
                principalColumn: "Id");
        }
    }
}
