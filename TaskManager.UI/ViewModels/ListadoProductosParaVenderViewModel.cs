using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoProductosParaVenderViewModel
    {
        public IEnumerable<ProductosParaVenderModel> ListadoProductosParaVender {  get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    }
}
