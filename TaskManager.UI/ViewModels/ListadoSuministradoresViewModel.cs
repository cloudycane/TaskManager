using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoSuministradoresViewModel
    {
        public IEnumerable<SuministradorModel> ListadoSuministradores { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    
    }
}
