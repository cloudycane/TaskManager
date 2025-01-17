using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface ISuministradorService
    {
        SuministradorDTO ConvertToDTO(SuministradorModel model);
        SuministradorModel ConvertToModel(SuministradorDTO dto);
        Task<List<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync();
        //Task<IEnumerable<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync();
    }
}
