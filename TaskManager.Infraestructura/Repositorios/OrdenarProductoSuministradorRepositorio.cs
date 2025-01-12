using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;
using OfficeOpenXml;

namespace TaskManager.Infraestructura.Repositorios
{
    public class OrdenarProductoSuministradorRepositorio : IOrdenarProductoSuministradorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public OrdenarProductoSuministradorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }   

        // OBTENER LISTADO ORDENES 
        public async Task<IEnumerable<OrdenAdquisicionModel>> ObtenerListadoOrdenarProductoSuministradorAsync()
        {
            return await _context.OrdenesAdquisicion.Include(o => o.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();
        }

        // OBTENER ORDEN POR ID

        public async Task<OrdenAdquisicionModel> ObtenerOrdenProductoPorIdAsync(int id)
        {
           return await _context.OrdenesAdquisicion.FindAsync(id);
        }

        // CSV

        public async Task<MemoryStream> ObtenerListadoOrdenesProductosSuministradoresCsv()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(OrdenAdquisicionModel.ProductoSuministrador.NombreProducto));
            csvWriter.WriteField(nameof(OrdenAdquisicionModel.PrecioTotal));
            csvWriter.WriteField(nameof(OrdenAdquisicionModel.CantidadDeOrden));
            csvWriter.WriteField(nameof(OrdenAdquisicionModel.ProductoSuministrador.Suministrador.RazonSocial));
         
            csvWriter.NextRecord();

            List<OrdenAdquisicionModel> ordenProductosSuministradores = await _context.OrdenesAdquisicion.Include(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();

            foreach (OrdenAdquisicionModel ordenProductoSuministrador in ordenProductosSuministradores)
            {
                csvWriter.WriteField(ordenProductoSuministrador.ProductoSuministrador.NombreProducto);
                csvWriter.WriteField(ordenProductoSuministrador.PrecioTotal);
                csvWriter.WriteField(ordenProductoSuministrador.CantidadDeOrden);
                csvWriter.WriteField(ordenProductoSuministrador.ProductoSuministrador.Suministrador.RazonSocial);
               
                csvWriter.NextRecord();
                csvWriter.Flush();
            }

            memoryStream.Position = 0;
            return memoryStream;

        }

        // EXCEL

        public async Task<MemoryStream> ObtenerListadoOrdenesProductosSuministradoresExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("LibroOrdenesProductosSuministradores");
                worksheet.Cells["A1"].Value = nameof(OrdenAdquisicionModel.ProductoSuministrador.NombreProducto);
                worksheet.Cells["B1"].Value = nameof(OrdenAdquisicionModel.PrecioTotal);
                worksheet.Cells["C1"].Value = nameof(OrdenAdquisicionModel.CantidadDeOrden);
                worksheet.Cells["D1"].Value = nameof(OrdenAdquisicionModel.ProductoSuministrador.Suministrador.RazonSocial);
          


                using (ExcelRange headerCells = worksheet.Cells["A1:D1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;

                List<OrdenAdquisicionModel> ordenProductosSuministradores = await _context.OrdenesAdquisicion.Include(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();

                foreach (var ordenProductoSuministrador in ordenProductosSuministradores)
                {
                    worksheet.Cells[row, 1].Value = ordenProductoSuministrador.ProductoSuministrador.NombreProducto;
                    worksheet.Cells[row, 2].Value = ordenProductoSuministrador.PrecioTotal;
                    worksheet.Cells[row, 3].Value = ordenProductoSuministrador.CantidadDeOrden;
                    worksheet.Cells[row, 4].Value = ordenProductoSuministrador.ProductoSuministrador.Suministrador.RazonSocial;
                  
                    row++;
                }

                worksheet.Cells[$"A1:D{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();

            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
