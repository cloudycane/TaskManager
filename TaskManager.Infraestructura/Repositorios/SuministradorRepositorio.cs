using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;
using System.Globalization;
using OfficeOpenXml;

namespace TaskManager.Infraestructura.Repositorios
{
    public class SuministradorRepositorio : ISuministradorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public SuministradorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        #region OBTENER LISTADO DE SUMINISTRADORES 

        public async Task<List<SuministradorModel>> ObtenerListadoSuministradorAsync()
        {
            return await _context.Suministradores.ToListAsync();
        }

        #endregion

        #region OBTENER SUMINISTRADOR POR ID 

        public async Task<SuministradorModel> ObtenerSuministradorPorIdAsync(int id)
        {
            return await _context.Suministradores.FindAsync(id);
        }

        #endregion

        #region CREAR SUMINISTRADOR

        public async Task CrearSuministradorAsync(SuministradorModel suministrador)
        {
            await _context.Suministradores.AddAsync(suministrador);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region ELIMINAR SUMINISTRADOR

        public async Task EliminarSuministrador(int id)
        {
            var suministrador = await ObtenerSuministradorPorIdAsync(id);

            if (suministrador != null)
            {
                _context.Suministradores.Remove(suministrador);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region ACTUALIZAR SUMINISTRADOR
        
        public async Task ActualizarSuministrador(SuministradorModel suministrador)
        {
            var suministradorAnterior = await ObtenerSuministradorPorIdAsync(suministrador.Id);

            if (suministradorAnterior != null)
            {
                suministradorAnterior.RazonSocial = suministrador.RazonSocial;
                suministradorAnterior.DireccionLinea1 = suministrador.DireccionLinea1;
                suministradorAnterior.DireccionLinea2 = suministrador.DireccionLinea2;
                suministradorAnterior.Localidad = suministrador.Localidad;
                suministradorAnterior.Pais = suministrador.Pais;    
                suministradorAnterior.Provincia = suministrador.Provincia;
                await _context.SaveChangesAsync();
            }
        }

        #endregion


        public async Task<MemoryStream> ObtenerListadoSuministradorCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(SuministradorModel.RazonSocial));
            csvWriter.WriteField(nameof(SuministradorModel.DireccionLinea1));
            csvWriter.WriteField(nameof(SuministradorModel.DireccionLinea2));
            csvWriter.WriteField(nameof(SuministradorModel.Localidad));
            csvWriter.WriteField(nameof(SuministradorModel.Provincia));
            csvWriter.WriteField(nameof(SuministradorModel.Pais));
            csvWriter.NextRecord();

            List<SuministradorModel> suministradores = await _context.Suministradores.ToListAsync();

            foreach(SuministradorModel suministrador in suministradores)
            {
                csvWriter.WriteField(suministrador.RazonSocial);
                csvWriter.WriteField(suministrador.DireccionLinea1);
                csvWriter.WriteField(suministrador.DireccionLinea2);
                csvWriter.WriteField(suministrador.Localidad);
                csvWriter.WriteField(suministrador.Provincia);
                csvWriter.WriteField(suministrador.Pais);
                csvWriter.NextRecord();
                csvWriter.Flush();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<MemoryStream> ObtenerListadoSuministradorExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("LibroSuministradores");
                worksheet.Cells["A1"].Value = nameof(SuministradorModel.RazonSocial);
                worksheet.Cells["B1"].Value = nameof(SuministradorModel.DireccionLinea1);
                worksheet.Cells["C1"].Value = nameof(SuministradorModel.DireccionLinea2);
                worksheet.Cells["D1"].Value = nameof(SuministradorModel.Localidad);
                worksheet.Cells["E1"].Value = nameof(SuministradorModel.Provincia);
                worksheet.Cells["F1"].Value = nameof(SuministradorModel.Pais);
          

                using (ExcelRange headerCells = worksheet.Cells["A1:F1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;

                List<SuministradorModel> suministradores = await _context.Suministradores.ToListAsync();
                
                foreach(var suministrador in suministradores)
                {
                    worksheet.Cells[row, 1].Value = suministrador.RazonSocial;
                    worksheet.Cells[row, 2].Value = suministrador.DireccionLinea1;
                    worksheet.Cells[row, 3].Value = suministrador.DireccionLinea2;
                    worksheet.Cells[row, 4].Value = suministrador.Localidad;
                    worksheet.Cells[row, 5].Value = suministrador.Provincia;
                    worksheet.Cells[row, 6].Value = suministrador.Pais;
                    row++;
                }

                worksheet.Cells[$"A1:F{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();
            
            }
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
