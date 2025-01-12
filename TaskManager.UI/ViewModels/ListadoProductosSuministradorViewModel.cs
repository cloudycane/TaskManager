using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoProductosSuministradorViewModel
    {
        public IEnumerable<ProductoSuministradorModel> Productos { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    }
}
