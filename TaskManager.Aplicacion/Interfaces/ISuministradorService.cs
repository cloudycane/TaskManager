using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface ISuministradorService
    {
        SuministradorDTO ConvertToDTO(SuministradorModel model);
        SuministradorModel ConvertToModel(SuministradorDTO dto);
        Task<SuministradorModel> CrearSuministradorAsync(SuministradorModel suministrador);
        Task<List<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync();
        Task<SuministradorModel> ObtenerUnSuministradorPorIdAsync(int id);
        //Task<IEnumerable<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync();
    }
}
