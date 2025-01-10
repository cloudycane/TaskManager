using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CrearEntidadesIngrendientesyProductosParaVenderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingrendientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductosParaVenderModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrendientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductosParaVender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    IngrendienteId = table.Column<int>(type: "int", nullable: false),
                    IngrendientesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosParaVender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosParaVender_Ingrendientes_IngrendientesId",
                        column: x => x.IngrendientesId,
                        principalTable: "Ingrendientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingrendientes_ProductosParaVenderModelId",
                table: "Ingrendientes",
                column: "ProductosParaVenderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosParaVender_IngrendientesId",
                table: "ProductosParaVender",
                column: "IngrendientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingrendientes_ProductosParaVender_ProductosParaVenderModelId",
                table: "Ingrendientes",
                column: "ProductosParaVenderModelId",
                principalTable: "ProductosParaVender",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingrendientes_ProductosParaVender_ProductosParaVenderModelId",
                table: "Ingrendientes");

            migrationBuilder.DropTable(
                name: "ProductosParaVender");

            migrationBuilder.DropTable(
                name: "Ingrendientes");
        }
    }
}
