using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoReservacionesViewModel
    {
        public IEnumerable<ReservacionModel> ListadoReservaciones { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    }
}
