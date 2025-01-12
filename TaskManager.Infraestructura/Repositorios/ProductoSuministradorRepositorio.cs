using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Globalization;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ProductoSuministradorRepositorio : IProductoSuministradorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ProductoSuministradorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO 

        public async Task<IEnumerable<ProductoSuministradorModel>> ObtenerListadoProductoSuministradorAsync()
        {
            return await _context.ProductosSuministradores.Include(s => s.Suministrador).ToListAsync();
        }

        // OBTENER POR ID
        
        public async Task<ProductoSuministradorModel> ObtenerProductoSuministradorPorIdAsync(int id)
        {
            return await _context.ProductosSuministradores.FindAsync(id);
        }

        // CREAR 

        public async Task<int> CrearProductoSuministradorAsync(ProductoSuministradorModel productoSuministrador)
        {
            await _context.ProductosSuministradores.AddAsync(productoSuministrador);
            await _context.SaveChangesAsync();
            return productoSuministrador.Id;
        }

        // ELIMINAR
        // 
        public async Task EliminarProductoSuministradorAsync (int id)
        {
            var productoSuministrador = await ObtenerProductoSuministradorPorIdAsync(id);

            if (productoSuministrador != null)
            {
                _context.ProductosSuministradores.Remove(productoSuministrador);
                await _context.SaveChangesAsync();
            }
        }

        // ACTUALIZAR

        public async Task ActualizarProductoSuministradorAsync(ProductoSuministradorModel productoSuministradormodel)
        {
            var productoSuministradorAnterior = await _context.ProductosSuministradores.FindAsync(productoSuministradormodel.Id);

            if (productoSuministradorAnterior != null)
            {
                productoSuministradorAnterior.NombreProducto = productoSuministradormodel.NombreProducto;
                productoSuministradorAnterior.DescripcionProducto = productoSuministradorAnterior.DescripcionProducto;
                productoSuministradorAnterior.PrecioProducto = productoSuministradorAnterior.PrecioProducto;
                productoSuministradorAnterior.CantidadProductoEnVenta = productoSuministradorAnterior.CantidadProductoEnVenta;
                productoSuministradorAnterior.UnidadProductoEnum = productoSuministradorAnterior.UnidadProductoEnum;
                productoSuministradorAnterior.SuministradorId = productoSuministradorAnterior.SuministradorId;
                productoSuministradorAnterior.CategoriaProductoSuministradorEnum = productoSuministradorAnterior.CategoriaProductoSuministradorEnum;
            }

            _context.ProductosSuministradores.Update(productoSuministradorAnterior);
            await _context.SaveChangesAsync();
        }

        // ORDENAR 

        public async Task OrdenarProducto(int productoId, int productoCantidad, OrdenAdquisicionModel ordenAdquisicion)
        {
            var productoSuministrador = await ObtenerProductoSuministradorPorIdAsync(productoId);
            var carritoItem = await _context.OrdenesAdquisicion.FirstOrDefaultAsync(o => o.ProductoSuministradorId == productoId);

            if (carritoItem != null && carritoItem.ProductoSuministrador != null)
            {
                carritoItem = new OrdenAdquisicionModel()
                {
                    Id = new int(),
                    ProductoSuministradorId = productoId,
                    Estado = 0,
                    CantidadDeOrden = productoCantidad *= carritoItem.CantidadDeOrden,
                    FechaDeCreacion = DateTime.Now,
                    HoraDeCreacion = TimeSpan.FromHours(DateTime.Now.Hour),
                    PrecioTotal = productoSuministrador.PrecioProducto * productoCantidad,

                };

                await _context.OrdenesAdquisicion.AddAsync(carritoItem);
                //carritoItem.CantidadDeOrden += productoCantidad;
                //carritoItem.PrecioTotal += productoSuministrador.PrecioProducto * productoCantidad;
            }
            else
            {
                carritoItem = new OrdenAdquisicionModel()
                {

                    ProductoSuministradorId = productoId,
                    Estado = 0,
                    CantidadDeOrden = productoCantidad,
                    FechaDeCreacion = DateTime.Now,
                    HoraDeCreacion = TimeSpan.FromHours(DateTime.Now.Hour),
                    PrecioTotal = productoSuministrador.PrecioProducto * productoCantidad,
                };

                await _context.OrdenesAdquisicion.AddAsync(carritoItem);
            }

            await _context.SaveChangesAsync();
        }

        // CSV 

       public async Task<MemoryStream> ObtenerProductoSuministradorCsv()
       {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(ProductoSuministradorModel.NombreProducto));
            csvWriter.WriteField(nameof(ProductoSuministradorModel.DescripcionProducto));
            csvWriter.WriteField(nameof(ProductoSuministradorModel.PrecioProducto));
            csvWriter.WriteField(nameof(ProductoSuministradorModel.CantidadProductoEnVenta));
            csvWriter.WriteField(nameof(ProductoSuministradorModel.UnidadProductoEnum));
            csvWriter.WriteField(nameof(ProductoSuministradorModel.CategoriaProductoSuministradorEnum));
            csvWriter.WriteField(nameof(ProductoSuministradorModel.Suministrador.RazonSocial));

            csvWriter.NextRecord();

            List<ProductoSuministradorModel> productoSuministradores = await _context.ProductosSuministradores.Include(s => s.Suministrador).ToListAsync();

            foreach (ProductoSuministradorModel productoSuministrador in productoSuministradores)
            {
                csvWriter.WriteField(productoSuministrador.NombreProducto);
                csvWriter.WriteField(productoSuministrador.DescripcionProducto);
                csvWriter.WriteField(productoSuministrador.PrecioProducto);
                csvWriter.WriteField(productoSuministrador.CantidadProductoEnVenta);
                csvWriter.WriteField(productoSuministrador.UnidadProductoEnum);
                csvWriter.WriteField(productoSuministrador.CategoriaProductoSuministradorEnum);
                csvWriter.WriteField(productoSuministrador.Suministrador.RazonSocial);
                csvWriter.NextRecord();
                csvWriter.Flush();
            }

            memoryStream.Position = 0;
            return memoryStream;

        }

        // EXCEL 

        public async Task<MemoryStream> ObtenerListadoProductoSuministradorExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("LibroProductosSuministradores");
                worksheet.Cells["A1"].Value = nameof(ProductoSuministradorModel.NombreProducto);
                worksheet.Cells["B1"].Value = nameof(ProductoSuministradorModel.DescripcionProducto);
                worksheet.Cells["C1"].Value = nameof(ProductoSuministradorModel.PrecioProducto);
                worksheet.Cells["D1"].Value = nameof(ProductoSuministradorModel.CantidadProductoEnVenta);
                worksheet.Cells["E1"].Value = nameof(ProductoSuministradorModel.UnidadProductoEnum);
                worksheet.Cells["F1"].Value = nameof(ProductoSuministradorModel.CategoriaProductoSuministradorEnum);
                worksheet.Cells["G1"].Value = nameof(ProductoSuministradorModel.Suministrador.RazonSocial);


                using (ExcelRange headerCells = worksheet.Cells["A1:G1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;

                List<ProductoSuministradorModel> productosSuministradores = await _context.ProductosSuministradores.Include(s => s.Suministrador).ToListAsync();

                foreach (var productoSuministrador in productosSuministradores)
                {
                    worksheet.Cells[row, 1].Value = productoSuministrador.NombreProducto;
                    worksheet.Cells[row, 2].Value = productoSuministrador.DescripcionProducto;
                    worksheet.Cells[row, 3].Value = productoSuministrador.PrecioProducto;
                    worksheet.Cells[row, 4].Value = productoSuministrador.CantidadProductoEnVenta;
                    worksheet.Cells[row, 5].Value = productoSuministrador.UnidadProductoEnum;
                    worksheet.Cells[row, 6].Value = productoSuministrador.CategoriaProductoSuministradorEnum;
                    worksheet.Cells[row, 7].Value = productoSuministrador.Suministrador.RazonSocial;
                    row++;
                }

                worksheet.Cells[$"A1:G{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();

            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }

}
