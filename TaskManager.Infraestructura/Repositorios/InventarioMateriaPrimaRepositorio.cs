using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Enum;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class InventarioMateriaPrimaRepositorio : IInventarioMateriaPrimaRepositorio 
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompraProductoSuministradorFacturacionRepositorio _compraProductoSuministradorFacturacion;

        public InventarioMateriaPrimaRepositorio(ApplicationDbContext context, ICompraProductoSuministradorFacturacionRepositorio compraProductoSuministradorFacturacion)
        {
            _context = context;
            _compraProductoSuministradorFacturacion = compraProductoSuministradorFacturacion;
        }

        // OBTENER LISTADO DE INVENTARIO 

        public async Task<IEnumerable<InventarioMateriaPrimaModel>> ObtenerListadoInventarioMateriaPrimaAsync()
        {
            return await _context.InventarioMateriaPrimas.Include(c => c.CompraProductoSuministrador).ThenInclude(o => o.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();
        }
        // AÑADIR AL INVENTARIO 

        public async Task AñadirAlInventarioAsync(int compraId, InventarioMateriaPrimaModel inventarioModel)
        {
            var compra = await _compraProductoSuministradorFacturacion.ObtenerListadoCompraProductoSuministradorPorIdAsync(compraId);

            if (compra != null)
            {
                compra.EstadoDeCompra = EstadoDeCompraEnum.ProductoRecibido;
                _context.CompraProductoSuministradorFacturacion.Update(compra);
                await _context.SaveChangesAsync();
            }

            var materiaPrima = await _context.InventarioMateriaPrimas.FirstOrDefaultAsync(c => c.CompraProductoSuministradorId == compraId);
            
            if (materiaPrima == null)
            {
                materiaPrima = new InventarioMateriaPrimaModel()
                {
                    CompraProductoSuministradorId = compraId,
                    CategoriaProductoInventario = CategoriaProductoInventarioEnum.CategoriaNoEspecificado,
                    EstadoProductoInventario = EstadoProductoInventarioEnum.NoEspecificado,
                    FechaDistribucion = DateTime.Now, 
                    CantidadProductoEnInventario = compra.OrdenAdquisicion.CantidadDeOrden
                    
                };

                await _context.InventarioMateriaPrimas.AddAsync(materiaPrima);
            }
            else
            {
                materiaPrima.CantidadProductoEnInventario += compra.OrdenAdquisicion.CantidadDeOrden;
            }

            await _context.InventarioMateriaPrimas.AddAsync(materiaPrima);
            await _context.SaveChangesAsync();

        }
    }
}
