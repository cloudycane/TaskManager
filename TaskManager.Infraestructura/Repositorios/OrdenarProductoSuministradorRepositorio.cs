using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class OrdenarProductoSuministradorRepositorio : IOrdenarProductoSuministradorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public OrdenarProductoSuministradorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }   

        // OBTENER LISTADO ORDENES 
        public async Task<IEnumerable<OrdenAdquisicionModel>> ObtenerListadoOrdenarProductoSuministradorAsync()
        {
            return await _context.OrdenesAdquisicion.Include(o => o.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();
        }

        // OBTENER ORDEN POR ID

        public async Task<OrdenAdquisicionModel> ObtenerOrdenProductoPorIdAsync(int id)
        {
           return await _context.OrdenesAdquisicion.FindAsync(id);
        }
    }
}
