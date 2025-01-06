using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ReservacionRepositorio : IReservacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservacionService _reservacionService;

        // INSTANCIAR 

        public ReservacionRepositorio(ApplicationDbContext context, ReservacionService reservacionService)
        {
            _context = context;
            _reservacionService = reservacionService;
        }

        // OBTENER LISTADO (READ) 

        public async Task<IEnumerable<ReservacionModel>> ObtenerListadoReservacion()
        {
            return await _context.Reservaciones.ToListAsync();
        }

        // OBTENER POR ID

        public async Task<ReservacionModel> ObtenerReservacionPorId(int id)
        {
            return await _context.Reservaciones.FindAsync(id);
        }


        // OBTENER POR FECHA

        public async Task<ReservacionModel> ObtenerReservacionPorFecha(DateTime fecha)
        {
            return await _context.Reservaciones.Where(r => r.FechadeReservacion == fecha).FirstOrDefaultAsync();
		}

        // CREAR 
        public async Task<int> CrearReservacionAsync(ReservacionModel reservacion)
        {
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
            return reservacion.Id;
		}

        // ELIMINAR 

        public async Task EliminarReservacion(int id)
        {
            var reservacion = await ObtenerReservacionPorId(id);

            if (reservacion != null)
            {
                _context.Reservaciones.Remove(reservacion);
                await _context.SaveChangesAsync();
            }
        }

        // EDITAR 

        public async Task EditarReservacion(ReservacionModel reservacionModel)
        {

            var reservacionAnterior = await ObtenerReservacionPorId(reservacionModel.Id);

            if (reservacionAnterior != null)
            {
                reservacionAnterior.Nombre = reservacionModel.Nombre;
                reservacionAnterior.Apellidos = reservacionModel.Apellidos;
                reservacionAnterior.Telefono = reservacionModel.Telefono;
                reservacionAnterior.CorreoElectronico = reservacionModel.CorreoElectronico;
                reservacionAnterior.FechadeReservacion = reservacionModel.FechadeReservacion;
                reservacionAnterior.HoradeReservacion = reservacionModel.HoradeReservacion;
                reservacionAnterior.EstadoReservacionEnum = reservacionModel.EstadoReservacionEnum;

                _context.Reservaciones.Update(reservacionAnterior);
                await _context.SaveChangesAsync();
            }

        }

        // CSV HELPER

        public async Task<MemoryStream> ObtenerReservacionCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);
            csvWriter.WriteField(nameof(ReservacionModel.Nombre));
            csvWriter.WriteField(nameof(ReservacionModel.Apellidos));
            csvWriter.WriteField(nameof(ReservacionModel.Telefono));
            csvWriter.WriteField(nameof(ReservacionModel.CorreoElectronico));
            csvWriter.WriteField(nameof(ReservacionModel.FechadeReservacion));
            csvWriter.WriteField(nameof(ReservacionModel.HoradeReservacion));
            csvWriter.WriteField(nameof(ReservacionModel.EstadoReservacionEnum));
            csvWriter.NextRecord();

            List<ReservacionModel> reservaciones = await _context.Reservaciones.ToListAsync();

            foreach (ReservacionModel reservacion in reservaciones)
            {
                csvWriter.WriteField(reservacion.Nombre);
                csvWriter.WriteField(reservacion.Apellidos);
                csvWriter.WriteField(reservacion.Telefono);
                csvWriter.WriteField(reservacion.CorreoElectronico);
                csvWriter.WriteField(reservacion.FechadeReservacion);
                csvWriter.WriteField(reservacion.HoradeReservacion);
                csvWriter.WriteField(reservacion.EstadoReservacionEnum);
                csvWriter.NextRecord();
                csvWriter.Flush();
            }

            memoryStream.Position = 0;
            return memoryStream;

        }
        public async Task<MemoryStream> ObtenerReservacionExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Reservaciones");

                excelWorksheet.Cells["A1"].Value = nameof(ReservacionModel.Nombre);
                excelWorksheet.Cells["B1"].Value = nameof(ReservacionModel.Apellidos);
                excelWorksheet.Cells["C1"].Value = nameof(ReservacionModel.Telefono);
                excelWorksheet.Cells["D1"].Value = nameof(ReservacionModel.CorreoElectronico);
                excelWorksheet.Cells["E1"].Value = nameof(ReservacionModel.FechadeReservacion);
                excelWorksheet.Cells["F1"].Value = nameof(ReservacionModel.HoradeReservacion);
                excelWorksheet.Cells["G1"].Value = nameof(ReservacionModel.EstadoReservacionEnum);

                using (ExcelRange excelRange = excelWorksheet.Cells["A1:G1"])
                {
                    excelRange.Style.Font.Bold = true;
                    excelRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    excelRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                int row = 2;

                List<ReservacionModel> reservaciones = await _context.Reservaciones.ToListAsync();

                foreach (var reservacion in reservaciones)
                {
                    excelWorksheet.Cells[row, 1].Value = reservacion.Nombre;
                    excelWorksheet.Cells[row, 2].Value = reservacion.Apellidos;
                    excelWorksheet.Cells[row, 3].Value = reservacion.Telefono;
                    excelWorksheet.Cells[row, 4].Value = reservacion.CorreoElectronico;
                    excelWorksheet.Cells[row, 5].Value = reservacion.FechadeReservacion.ToString("d");
                    excelWorksheet.Cells[row, 6].Value = reservacion.HoradeReservacion.ToString("hh") + ":" + reservacion.HoradeReservacion.ToString("mm");
                    excelWorksheet.Cells[row, 7].Value = reservacion.EstadoReservacionEnum;
                    row++;
                }

                excelWorksheet.Cells[$"A1:G{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();
            };

            memoryStream.Position = 0;
            return memoryStream;
        }
    }

    

   
}

