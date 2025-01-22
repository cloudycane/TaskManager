using AutoMapper;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades.DTOs;
using TaskManager.Dominio.Entidades;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using OfficeOpenXml;

public class SuministradorService : ISuministradorService
{
    private readonly IMapper _mapper;
    private readonly ISuministradorRepositorio _suministradorRepositorio;
 
    public SuministradorService(IMapper mapper, ISuministradorRepositorio suministradorRepositorio)
    {
        _mapper = mapper;
        _suministradorRepositorio = suministradorRepositorio;
    }

    #region Convertir los modelos a DTO y vice versa.
    public SuministradorModel ConvertToModel(SuministradorDTO dto)
    {
        return _mapper.Map<SuministradorModel>(dto);
    }

    public SuministradorDTO ConvertToDTO(SuministradorModel model)
    {
        return _mapper.Map<SuministradorDTO>(model);
    }
    #endregion

    #region Método de servicio para obtener un listado de los suministradores
    public async Task<List<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync()
    {
        var listadoSuministradores = await _suministradorRepositorio.ObtenerListadoSuministradorAsync();
        return listadoSuministradores;
    }
    #endregion

    #region Método de servicio para obtener un suministrador por su Id 
    
    public async Task<SuministradorModel> ObtenerUnSuministradorPorIdAsync(int id)
    {
        var suministrador = await _suministradorRepositorio.ObtenerSuministradorPorIdAsync(id);
        return suministrador;
    }

    #endregion

    #region CREAR

    public async Task<SuministradorModel> CrearSuministradorAsync(SuministradorModel suministrador)
    {
       await _suministradorRepositorio.CrearSuministradorAsync(suministrador);
       var suministradorId = suministrador.Id;
       var suministradorCreado = await ObtenerUnSuministradorPorIdAsync(suministradorId);
       return suministradorCreado;
       
    }

    #endregion

    #region ELIMINAR 

    public async Task<bool> EliminarSuministradorModel(int id)
    {
        var suministrador = await ObtenerUnSuministradorPorIdAsync(id);

        if (suministrador == null)
        {
            return false;
        }

        await _suministradorRepositorio.EliminarSuministrador(id);
        return true;
    }


    #endregion

    #region ACTUALIZAR DATOS

    public async Task<bool> ActualizarSuministralModel(SuministradorModel suministrador)
    {
        var suministradorId = await ObtenerUnSuministradorPorIdAsync(suministrador.Id);

        if (suministrador == null)
        {
            return false;
        }

        await _suministradorRepositorio.ActualizarSuministrador(suministrador);
        return true;

    }

    #endregion

    #region GENERAR ARCHIVO CSV
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

        List<SuministradorModel> suministradores = await _suministradorRepositorio.ObtenerListadoSuministradorAsync();

        foreach (SuministradorModel suministrador in suministradores)
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

    #endregion

    #region GENERAR ARCHIVO EXCEL

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

            List<SuministradorModel> suministradores = await _suministradorRepositorio.ObtenerListadoSuministradorAsync();

            foreach (var suministrador in suministradores)
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

    #endregion
}
