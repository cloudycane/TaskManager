using TaskManager.Aplicacion.Interfaces;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class CompraProductoSuministradorFacturacionRepositorio : ICompraProductoSuministradorFacturacionRepositorio
    {
        private readonly ApplicationDbContext _context;

        public CompraProductoSuministradorFacturacionRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO 

        // OBTENER POR ID 

        // COMPRAR 

        // EDITAR 

        // ELIMINAR 

        // FACTURAR (PDF)

        // EXCEL 

        // CSV
    }
}
