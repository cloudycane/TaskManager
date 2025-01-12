using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoOrdenesAdquisicionViewModel
    {
        public IEnumerable<OrdenAdquisicionModel> OrdenesAdquisicion { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    }
}
