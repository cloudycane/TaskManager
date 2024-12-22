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
        Task CrearReservacionAsync(ReservacionModel reservacion);
        Task EditarReservacion(ReservacionModel reservacionModel);
        Task EliminarReservacion(int id);
        Task<IEnumerable<ReservacionModel>> ObtenerListadoReservacion();
        Task<ReservacionModel> ObtenerReservacionPorId(int id);
    }
}
