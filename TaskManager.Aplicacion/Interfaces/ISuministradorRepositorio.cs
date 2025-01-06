using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface ISuministradorRepositorio
    {
        Task ActualizarSuministrador(SuministradorModel suministrador);
        Task CrearSuministradorAsync(SuministradorModel suministrador);
        Task EliminarSuministrador(int id);
        Task<IEnumerable<SuministradorModel>> ObtenerListadoSuministradorAsync();
        Task<SuministradorModel> ObtenerSuministradorPorIdAsync(int id);
    }
}
