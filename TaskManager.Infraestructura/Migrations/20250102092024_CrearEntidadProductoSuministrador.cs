using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CrearEntidadProductoSuministrador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductosSuministradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescripcionProducto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioProducto = table.Column<double>(type: "float", nullable: false),
                    CantidadProductoEnVenta = table.Column<int>(type: "int", nullable: false),
                    UnidadProductoEnum = table.Column<int>(type: "int", nullable: false),
                    CategoriaProductoSuministradorEnum = table.Column<int>(type: "int", nullable: false),
                    SuministradorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosSuministradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosSuministradores_Suministradores_SuministradorId",
                        column: x => x.SuministradorId,
                        principalTable: "Suministradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosSuministradores_SuministradorId",
                table: "ProductosSuministradores",
                column: "SuministradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosSuministradores");
        }
    }
}
