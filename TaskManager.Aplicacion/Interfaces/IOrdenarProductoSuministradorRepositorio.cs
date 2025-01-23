using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IOrdenarProductoSuministradorRepositorio
    {
        Task ActualizarOrdenProductoSuministrador(OrdenAdquisicionModel ordenAdquisicion);
        Task EliminarOrdenProductoSuministrador(int id);
        Task<IEnumerable<OrdenAdquisicionModel>> ObtenerListadoOrdenarProductoSuministradorAsync();
        Task<MemoryStream> ObtenerListadoOrdenesProductosSuministradoresCsv();
        Task<MemoryStream> ObtenerListadoOrdenesProductosSuministradoresExcel();
        Task<OrdenAdquisicionModel> ObtenerOrdenProductoPorIdAsync(int id);
    }
}
