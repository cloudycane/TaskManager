using Microsoft.EntityFrameworkCore;
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

        #region 

        public async Task<List<PedidosClienteModel>> ObtenerListadoPedidosClientesAsync()
        {
            var result = await _context.PedidosClientes.Include(p => p.Pedidos).ToListAsync();
            return result;
        }


        #endregion

        // OBTENER POR ID

        // CREAR

        public async Task CrearPedidosCliente(PedidosClienteModel model)
        {
            model.FechaDePedido = DateTime.Now;
            await _context.PedidosClientes.AddAsync(model);
            await _context.SaveChangesAsync();
            
        }
    }
}
