using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class PedidosClienteRepositorio : IPedidosClienteRepositorio
    {
        private readonly ApplicationDbContext _context; 

        public PedidosClienteRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO

        // OBTENER POR ID

        // CREAR

        public async Task CrearPedidosCliente(PedidosClienteModel model)
        {
            await _context.PedidosClientes.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
