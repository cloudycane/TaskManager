using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infraestructura.Migrations
{
    public partial class CrearEntidadCompraProductoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompraProductoSuministradorModelId",
                table: "OrdenesAdquisicion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompraProductoSuministradorFacturacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturacionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdenAdquisicionId = table.Column<int>(type: "int", nullable: false),
                    NombreRepresentante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApellidoRepresentante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaginaWebEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoElectronicoEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreLegalDeLaEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionLinea1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DireccionLinea2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoDeCompra = table.Column<int>(type: "int", nullable: false),
                    MetodoDePago = table.Column<int>(type: "int", nullable: false),
                    FechaDeEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TasaDeIva = table.Column<double>(type: "float", nullable: true),
                    TasaDeTransporte = table.Column<double>(type: "float", nullable: true),
                    TasaDeDescuento = table.Column<double>(type: "float", nullable: true),
                    GastosDeMantenimiento = table.Column<double>(type: "float", nullable: true),
                    GastosDeEnvio = table.Column<double>(type: "float", nullable: true),
                    TerminosPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraProductoSuministradorFacturacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraProductoSuministradorFacturacion_OrdenesAdquisicion_OrdenAdquisicionId",
                        column: x => x.OrdenAdquisicionId,
                        principalTable: "OrdenesAdquisicion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesAdquisicion_CompraProductoSuministradorModelId",
                table: "OrdenesAdquisicion",
                column: "CompraProductoSuministradorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProductoSuministradorFacturacion_OrdenAdquisicionId",
                table: "CompraProductoSuministradorFacturacion",
                column: "OrdenAdquisicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesAdquisicion_CompraProductoSuministradorFacturacion_CompraProductoSuministradorModelId",
                table: "OrdenesAdquisicion",
                column: "CompraProductoSuministradorModelId",
                principalTable: "CompraProductoSuministradorFacturacion",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesAdquisicion_CompraProductoSuministradorFacturacion_CompraProductoSuministradorModelId",
                table: "OrdenesAdquisicion");

            migrationBuilder.DropTable(
                name: "CompraProductoSuministradorFacturacion");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesAdquisicion_CompraProductoSuministradorModelId",
                table: "OrdenesAdquisicion");

            migrationBuilder.DropColumn(
                name: "CompraProductoSuministradorModelId",
                table: "OrdenesAdquisicion");
        }
    }
}
