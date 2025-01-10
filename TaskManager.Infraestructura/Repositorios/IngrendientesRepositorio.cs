using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class IngrendientesRepositorio : IIngrendientesRepositorio
    {
        private readonly ApplicationDbContext _context;

        public IngrendientesRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO INGRENDIENTES 
        public async Task<List<IngrendientesModel>> ObtenerListadoIngrendientesAsync()
        {
            return await _context.Ingrendientes.ToListAsync();
        }
        // OBTENER INGRENDIENTE POR ID 

        // CREAR INGRENDIENTE 

        public async Task CrearIngrendienteAsync(IngrendientesModel model)
        {
            await _context.Ingrendientes.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        // ELIMINAR INGRENDIENTE 

        // EDITAR INGRENDIENTE 
    }
}
