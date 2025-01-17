using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.ViewModels
{
    public class ListadoSuministradoresViewModel
    {
        public List<SuministradorModel> ListadoSuministradores { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotal { get; set; }
    
    }
}
