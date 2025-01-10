using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class EliminarRelacionEntreEntidadesIngredientesyProductosParaVenderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingrendientes_ProductosParaVender_ProductosParaVenderModelId",
                table: "Ingrendientes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductosParaVender_Ingrendientes_IngrendientesId",
                table: "ProductosParaVender");

            migrationBuilder.DropIndex(
                name: "IX_ProductosParaVender_IngrendientesId",
                table: "ProductosParaVender");

            migrationBuilder.DropIndex(
                name: "IX_Ingrendientes_ProductosParaVenderModelId",
                table: "Ingrendientes");

            migrationBuilder.DropColumn(
                name: "IngrendienteId",
                table: "ProductosParaVender");

            migrationBuilder.DropColumn(
                name: "IngrendientesId",
                table: "ProductosParaVender");

            migrationBuilder.DropColumn(
                name: "ProductosParaVenderModelId",
                table: "Ingrendientes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngrendienteId",
                table: "ProductosParaVender",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IngrendientesId",
                table: "ProductosParaVender",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductosParaVenderModelId",
                table: "Ingrendientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosParaVender_IngrendientesId",
                table: "ProductosParaVender",
                column: "IngrendientesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrendientes_ProductosParaVenderModelId",
                table: "Ingrendientes",
                column: "ProductosParaVenderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrendientes_ProductosParaVender_ProductosParaVenderModelId",
                table: "Ingrendientes",
                column: "ProductosParaVenderModelId",
                principalTable: "ProductosParaVender",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosParaVender_Ingrendientes_IngrendientesId",
                table: "ProductosParaVender",
                column: "IngrendientesId",
                principalTable: "Ingrendientes",
                principalColumn: "Id");
        }
    }
}
