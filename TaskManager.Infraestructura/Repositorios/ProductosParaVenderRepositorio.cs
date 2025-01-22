using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ProductosParaVenderRepositorio : IProductosParaVenderRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ProductosParaVenderRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO PRODUCTOS PARA VENDER 
         
        public async Task<IEnumerable<ProductosParaVenderModel>> ObtenerListadoProductosParaVenderAsync()
        {
            return await _context.ProductosParaVender.Include(i => i.Ingredientes).ToListAsync();
        }
 

        // OBTENER PRODUCTO POR ID 

        // CREAR PRODUCTO 

        public async Task CrearProductoParaVender(ProductosParaVenderModel productosParaVender)
        {
            await _context.ProductosParaVender.AddAsync(productosParaVender);
            await _context.SaveChangesAsync();
        }

        // CSV 

        public async Task<MemoryStream> ObtenerProductosParaVenderCsv()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvConfiguration csvConfiguration = new CsvConfiguration(cultureInfo: System.Globalization.CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(ProductosParaVenderModel.NombreProducto));
            csvWriter.WriteField(nameof(ProductosParaVenderModel.Descripcion));
            csvWriter.WriteField(nameof(ProductosParaVenderModel.Precio));
            csvWriter.WriteField(nameof(ProductosParaVenderModel.CategoriaProductoEnVentas));
            
            await csvWriter.NextRecordAsync();

            List<ProductosParaVenderModel> productosParaVender = await _context.ProductosParaVender.Include(i => i.Ingredientes).ToListAsync();

            foreach (ProductosParaVenderModel producto in productosParaVender)
            {
                csvWriter.WriteField(producto.NombreProducto);
                csvWriter.WriteField(producto.Descripcion);
                csvWriter.WriteField(producto.Precio);
                csvWriter.WriteField(producto.CategoriaProductoEnVentas.ToString());


                await csvWriter.NextRecordAsync();
                await csvWriter.FlushAsync();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        // EXCEL 

        public async Task<MemoryStream> ObtenerListadoProductosParaVenderExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("ListadoProductosParaVenderSheet");
                worksheet.Cells["A1"].Value = "Nombre Producto";
                worksheet.Cells["B1"].Value = "Descripción";
                worksheet.Cells["C1"].Value = "Precio";
         
                worksheet.Cells["D1"].Value = "Categoría Producto";
                

                using (ExcelRange headerCells = worksheet.Cells["A1:D1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;

                List<ProductosParaVenderModel> productosParaVender = await _context.ProductosParaVender.Include(i => i.Ingredientes).ToListAsync();

                foreach (ProductosParaVenderModel producto in productosParaVender)
                {
                    worksheet.Cells[row, 1].Value = producto.NombreProducto;
                    worksheet.Cells[row, 2].Value = producto.Descripcion;
                    worksheet.Cells[row, 3].Value = producto.Precio;
                
                    worksheet.Cells[row, 4].Value = producto.CategoriaProductoEnVentas;
                   
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
