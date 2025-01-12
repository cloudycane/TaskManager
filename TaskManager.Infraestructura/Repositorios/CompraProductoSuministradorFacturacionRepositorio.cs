using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Enum;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class CompraProductoSuministradorFacturacionRepositorio : ICompraProductoSuministradorFacturacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrdenarProductoSuministradorRepositorio _ordenarProductoSuministradorRepositorio;

        public CompraProductoSuministradorFacturacionRepositorio(ApplicationDbContext context, IOrdenarProductoSuministradorRepositorio ordenarProductoSuministradorRepositorio)
        {
            _context = context;
            _ordenarProductoSuministradorRepositorio = ordenarProductoSuministradorRepositorio;
        }

        // OBTENER LISTADO 

        public async Task<IEnumerable<CompraProductoSuministradorModel>> ObtenerListadoCompraProductoSuministradorAsync()
        {
            return await _context.CompraProductoSuministradorFacturacion.Include(o => o.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();
        }

        // OBTENER POR ID 

        public async Task<CompraProductoSuministradorModel> ObtenerListadoCompraProductoSuministradorPorIdAsync(int id)
        {
            return await _context.CompraProductoSuministradorFacturacion.Include(o => o.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).Where(c => c.Id == id)
                         .FirstOrDefaultAsync();
        }

        // COMPRAR 

        public async Task ComprarAsync(int ordenId, CompraProductoSuministradorModel compraProductoSuministrador)
        {

            // 1 - MOCK DATA 
            // 2 - Return RedirectToAction - Comprar Method - Form

            var orden = await _ordenarProductoSuministradorRepositorio.ObtenerOrdenProductoPorIdAsync(ordenId);
            if (orden != null)
            {
                orden.Estado = EstadoOrdenProductoSuministradorEnum.Comprado;
                _context.OrdenesAdquisicion.Update(orden);
                await _context.SaveChangesAsync();
            }

            var compra = await _context.CompraProductoSuministradorFacturacion.FirstOrDefaultAsync(c => c.OrdenAdquisicionId == ordenId);

            compra = new CompraProductoSuministradorModel()
            {
                CIF = "12345678A", 
                DireccionLinea1 = "Calle Falsa 123",
                DireccionLinea2 = "Avenida Algo",
                Ciudad = "Villaviciosa",
                CodigoPostal = "28000",
                CorreoElectronicoEmpresa = "restofibonacci@gmail.com", 
                EstadoDeCompra = EstadoDeCompraEnum.PendienteDePago, 
                FechaDeEmision = DateTime.Now,
                Localidad = "Ejemplo", 
                MetodoDePago = MetodoDePagoEnum.NoEspecificado,
                NombreLegalDeLaEmpresa = "Resto Fibonacci",
                OrdenAdquisicionId = ordenId, 
                PaginaWebEmpresa = "www.restofibonacci.com", 
                Provincia = "Madrid", 
                TerminosPago = TerminosPagoEnum.noEspecificado,
                TelefonoEmpresa = "123456789"
            };

            await _context.CompraProductoSuministradorFacturacion.AddAsync(compra);
            await _context.SaveChangesAsync();
        }

        // EDITAR 

        // ELIMINAR 

        public async Task EliminarCompra(int id)
        {
            var compra = await ObtenerListadoCompraProductoSuministradorPorIdAsync(id);

            if (compra != null)
            {
                _context.CompraProductoSuministradorFacturacion.Remove(compra);
                await _context.SaveChangesAsync();
            }
        }

        // FACTURAR (PDF)

        // EXCEL 

        public async Task<MemoryStream> ObtenerComprasProductosSuministradoresExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("ComprasProductosSuministradoreSheet");
                worksheet.Cells["A1"].Value = "Nombre Producto";
                worksheet.Cells["B1"].Value = "Precio Total";
                worksheet.Cells["C1"].Value = "Cantidad";
                worksheet.Cells["D1"].Value = "Unidad";
                worksheet.Cells["E1"].Value = "Categoría";
                worksheet.Cells["F1"].Value = "Proveedor";
                worksheet.Cells["G1"].Value = "Fecha de Emisión";

                using (ExcelRange headerCells = worksheet.Cells["A1:G1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;

                IEnumerable<CompraProductoSuministradorModel> compras = await ObtenerListadoCompraProductoSuministradorAsync();

                foreach(CompraProductoSuministradorModel compra in compras)
                {
                    worksheet.Cells[row, 1].Value = compra.OrdenAdquisicion.ProductoSuministrador.NombreProducto;
                    worksheet.Cells[row, 2].Value = compra.OrdenAdquisicion.PrecioTotal;
                    worksheet.Cells[row, 3].Value = compra.OrdenAdquisicion.CantidadDeOrden;
                    worksheet.Cells[row, 4].Value = compra.OrdenAdquisicion.ProductoSuministrador.UnidadProductoEnum;
                    worksheet.Cells[row, 5].Value = compra.OrdenAdquisicion.ProductoSuministrador.CategoriaProductoSuministradorEnum;
                    worksheet.Cells[row, 6].Value = compra.OrdenAdquisicion.ProductoSuministrador.Suministrador.RazonSocial;
                    worksheet.Cells[row, 7].Value = compra.FechaDeEmision;

                    row++;
                }

                worksheet.Cells[$"A1:H{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();
            }
            memoryStream.Position = 0;
            return memoryStream; 
        }

        // CSV

        public async Task<MemoryStream> ObtenerCompraProductoCsv ()
        {
            MemoryStream memoryStream = new MemoryStream(); 
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvConfiguration csvConfiguration = new CsvConfiguration(cultureInfo: System.Globalization.CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.OrdenAdquisicion.ProductoSuministrador.NombreProducto));
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.OrdenAdquisicion.PrecioTotal));
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.OrdenAdquisicion.CantidadDeOrden));
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.OrdenAdquisicion.ProductoSuministrador.UnidadProductoEnum));
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.OrdenAdquisicion.ProductoSuministrador.CategoriaProductoSuministradorEnum));
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.OrdenAdquisicion.ProductoSuministrador.Suministrador.RazonSocial));
            csvWriter.WriteField(nameof(CompraProductoSuministradorModel.FechaDeEmision));
            await csvWriter.NextRecordAsync();

            IEnumerable<CompraProductoSuministradorModel> compras = await ObtenerListadoCompraProductoSuministradorAsync();

            foreach (CompraProductoSuministradorModel compra in compras)
            {
                csvWriter.WriteField(compra.OrdenAdquisicion.ProductoSuministrador.NombreProducto);
                csvWriter.WriteField(compra.OrdenAdquisicion.PrecioTotal);
                csvWriter.WriteField(compra.OrdenAdquisicion.CantidadDeOrden);
                csvWriter.WriteField(compra.OrdenAdquisicion.ProductoSuministrador.UnidadProductoEnum);
                csvWriter.WriteField(compra.OrdenAdquisicion.ProductoSuministrador.CategoriaProductoSuministradorEnum);
                csvWriter.WriteField(compra.OrdenAdquisicion.ProductoSuministrador.Suministrador.RazonSocial);
                csvWriter.WriteField(compra.FechaDeEmision);
                await csvWriter.NextRecordAsync();
                await csvWriter.FlushAsync(); 
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
