using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoInventarioMateriaPrimaViewModel
    {
        public IEnumerable<InventarioMateriaPrimaModel> ListadoInventarioMateriasPrimasViewModel { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    }
}
