using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface ISuministradorService
    {
        Task<bool> ActualizarSuministralModel(SuministradorModel suministrador);

        //Task<bool> ActualizarSuministralModel(int id);
        SuministradorDTO ConvertToDTO(SuministradorModel model);
        SuministradorModel ConvertToModel(SuministradorDTO dto);
        Task<SuministradorModel> CrearSuministradorAsync(SuministradorModel suministrador);
        Task<bool> EliminarSuministradorModel(int id);
        Task<List<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync();
        Task<MemoryStream> ObtenerListadoSuministradorCSV();
        Task<MemoryStream> ObtenerListadoSuministradorExcel();
        Task<SuministradorModel> ObtenerUnSuministradorPorIdAsync(int id);
        //Task<IEnumerable<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync();
    }
}
