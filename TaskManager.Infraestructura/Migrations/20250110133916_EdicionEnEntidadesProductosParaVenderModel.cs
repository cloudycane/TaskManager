using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class EdicionEnEntidadesProductosParaVenderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductosParaVenderModelId",
                table: "ProductosSuministradores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDeProduccion",
                table: "ProductosParaVender",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ProductosParaVender",
                type: "nvarchar(max)",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FechaDeProduccion",
                table: "ProductosParaVender");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ProductosParaVender");
        }
    }
}
