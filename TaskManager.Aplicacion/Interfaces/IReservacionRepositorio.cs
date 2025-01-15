using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IReservacionRepositorio
    {
		Task<int> CrearReservacionAsync(ReservacionModel reservacion);
		Task EditarReservacion(ReservacionModel reservacionModel);
        Task EliminarReservacion(int id);
        Task<IEnumerable<ReservacionModel>> ObtenerListadoReservacion();
        Task<MemoryStream> ObtenerReservacionCSV();
        Task<MemoryStream> ObtenerReservacionExcel();
        Task<ReservacionModel> ObtenerReservacionPorFecha(DateTime fecha);

        //Task<ReservacionModel> ObtenerReservacionPorFecha(DateTime fecha);
        Task<ReservacionModel> ObtenerReservacionPorId(int id);
    }
}
