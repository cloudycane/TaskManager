using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Enum;
using TaskManager.Infraestructura.Data;
using OfficeOpenXml;

namespace TaskManager.Infraestructura.Repositorios
{
    public class InventarioMateriaPrimaRepositorio : IInventarioMateriaPrimaRepositorio 
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompraProductoSuministradorFacturacionRepositorio _compraProductoSuministradorFacturacion;

        public InventarioMateriaPrimaRepositorio(ApplicationDbContext context, ICompraProductoSuministradorFacturacionRepositorio compraProductoSuministradorFacturacion)
        {
            _context = context;
            _compraProductoSuministradorFacturacion = compraProductoSuministradorFacturacion;
        }

        // OBTENER LISTADO DE INVENTARIO 

        public async Task<IEnumerable<InventarioMateriaPrimaModel>> ObtenerListadoInventarioMateriaPrimaAsync()
        {
            return await _context.InventarioMateriaPrimas.Include(c => c.CompraProductoSuministrador).ThenInclude(o => o.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();
        }
        // AÑADIR AL INVENTARIO 

        public async Task AñadirAlInventarioAsync(int compraId, InventarioMateriaPrimaModel inventarioModel)
        {
            var compra = await _compraProductoSuministradorFacturacion.ObtenerListadoCompraProductoSuministradorPorIdAsync(compraId);

            if (compra != null)
            {
                compra.EstadoDeCompra = EstadoDeCompraEnum.ProductoRecibido;
                _context.CompraProductoSuministradorFacturacion.Update(compra);
                await _context.SaveChangesAsync();
            }

            var materiaPrima = await _context.InventarioMateriaPrimas.FirstOrDefaultAsync(c => c.CompraProductoSuministradorId == compraId);
            
            if (materiaPrima == null)
            {
                materiaPrima = new InventarioMateriaPrimaModel()
                {
                    CompraProductoSuministradorId = compraId,
                    CategoriaProductoInventario = CategoriaProductoInventarioEnum.CategoriaNoEspecificado,
                    EstadoProductoInventario = EstadoProductoInventarioEnum.NoEspecificado,
                    FechaDistribucion = DateTime.Now, 
                    CantidadProductoEnInventario = compra.OrdenAdquisicion.CantidadDeOrden
                    
                };

                await _context.InventarioMateriaPrimas.AddAsync(materiaPrima);
            }
            else
            {
                materiaPrima.CantidadProductoEnInventario += compra.OrdenAdquisicion.CantidadDeOrden;
            }

            await _context.InventarioMateriaPrimas.AddAsync(materiaPrima);
            await _context.SaveChangesAsync();

        }

        // CSV 

        public async Task<MemoryStream> ObtenerInventarioMateriaPrimaCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(InventarioMateriaPrimaModel.CompraProductoSuministrador.OrdenAdquisicion.ProductoSuministrador.NombreProducto));
            csvWriter.WriteField(nameof(InventarioMateriaPrimaModel.CantidadProductoEnInventario));
            csvWriter.WriteField(nameof(InventarioMateriaPrimaModel.CategoriaProductoInventario));
            csvWriter.WriteField(nameof(InventarioMateriaPrimaModel.EstadoProductoInventario));

            csvWriter.NextRecord();

            List<InventarioMateriaPrimaModel> inventarioMateriaPrimas = await _context.InventarioMateriaPrimas.Include(p => p.CompraProductoSuministrador).ThenInclude(s => s.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();


            foreach (var materiaPrima in inventarioMateriaPrimas.GroupBy(p => new
            {
                p.CompraProductoSuministrador.OrdenAdquisicion.ProductoSuministrador.NombreProducto,
                p.CategoriaProductoInventario,
                p.EstadoProductoInventario
            })
             .Select(g => new
             {
                 NombreProducto = g.Key.NombreProducto,
                 CantidadTotal = g.Sum(p => p.CantidadProductoEnInventario),
                 Categoria = g.Key.CategoriaProductoInventario,
                 Estado = g.Key.EstadoProductoInventario
             }))
            {
                csvWriter.WriteField(materiaPrima.NombreProducto);
                csvWriter.WriteField(materiaPrima.CantidadTotal);
                csvWriter.WriteField(materiaPrima.Categoria);
                csvWriter.WriteField(materiaPrima.Estado);

                csvWriter.NextRecord();
                csvWriter.Flush();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        // EXCEL

        public async Task<MemoryStream> ObtenerInventarioMateriaPrimaExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("InventarioMateriasPrimas");

                excelWorksheet.Cells["A1"].Value = nameof(InventarioMateriaPrimaModel.CompraProductoSuministrador.OrdenAdquisicion.ProductoSuministrador.NombreProducto);
                excelWorksheet.Cells["B1"].Value = nameof(InventarioMateriaPrimaModel.CantidadProductoEnInventario);
                excelWorksheet.Cells["C1"].Value = nameof(InventarioMateriaPrimaModel.CategoriaProductoInventario);
                excelWorksheet.Cells["D1"].Value = nameof(InventarioMateriaPrimaModel.EstadoProductoInventario);

                using (ExcelRange excelRange = excelWorksheet.Cells["A1:D1"])
                {
                    excelRange.Style.Font.Bold = true;
                    excelRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    excelRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                int row = 2;

                List<InventarioMateriaPrimaModel> inventarioMateriaPrimas = await _context.InventarioMateriaPrimas.Include(p => p.CompraProductoSuministrador).ThenInclude(s => s.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();


                foreach (var materiaPrima in inventarioMateriaPrimas.GroupBy(p => new
                {
                    p.CompraProductoSuministrador.OrdenAdquisicion.ProductoSuministrador.NombreProducto,
                    p.CategoriaProductoInventario,
                    p.EstadoProductoInventario
                })
                 .Select(g => new
                 {
                     NombreProducto = g.Key.NombreProducto,
                     CantidadTotal = g.Sum(p => p.CantidadProductoEnInventario),
                     Categoria = g.Key.CategoriaProductoInventario,
                     Estado = g.Key.EstadoProductoInventario
                 }))
                {
                    excelWorksheet.Cells[row, 1].Value = materiaPrima.NombreProducto;
                    excelWorksheet.Cells[row, 2].Value = materiaPrima.CantidadTotal;
                    excelWorksheet.Cells[row, 3].Value = materiaPrima.Categoria;
                    excelWorksheet.Cells[row, 4].Value = materiaPrima.Estado;
                    row++;
                }

                excelWorksheet.Cells[$"A1:D{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();
            };

            memoryStream.Position = 0;
            return memoryStream;
       
    }
    }
}
