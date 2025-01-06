using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class EditarPropiedadEnIEnumerableSuministrador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosSuministradores_Suministradores_SuministradorId",
                table: "ProductosSuministradores");

            migrationBuilder.DropIndex(
                name: "IX_ProductosSuministradores_SuministradorId",
                table: "ProductosSuministradores");

            migrationBuilder.AddColumn<int>(
                name: "ProductoSuministradorModelId",
                table: "Suministradores",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NombreProducto",
                table: "ProductosSuministradores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescripcionProducto",
                table: "ProductosSuministradores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suministradores_ProductoSuministradorModelId",
                table: "Suministradores",
                column: "ProductoSuministradorModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suministradores_ProductosSuministradores_ProductoSuministradorModelId",
                table: "Suministradores",
                column: "ProductoSuministradorModelId",
                principalTable: "ProductosSuministradores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suministradores_ProductosSuministradores_ProductoSuministradorModelId",
                table: "Suministradores");

            migrationBuilder.DropIndex(
                name: "IX_Suministradores_ProductoSuministradorModelId",
                table: "Suministradores");

            migrationBuilder.DropColumn(
                name: "ProductoSuministradorModelId",
                table: "Suministradores");

            migrationBuilder.AlterColumn<string>(
                name: "NombreProducto",
                table: "ProductosSuministradores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DescripcionProducto",
                table: "ProductosSuministradores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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
    }
}
