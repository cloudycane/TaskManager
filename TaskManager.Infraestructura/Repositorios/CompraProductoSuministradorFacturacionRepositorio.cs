using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Enum;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class CompraProductoSuministradorFacturacionRepositorio : ICompraProductoSuministradorFacturacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrdenarProductoSuministradorRepositorio _ordenarProductoSuministradorRepositorio;

        public CompraProductoSuministradorFacturacionRepositorio(ApplicationDbContext context, IOrdenarProductoSuministradorRepositorio ordenarProductoSuministradorRepositorio)
        {
            _context = context;
            _ordenarProductoSuministradorRepositorio = ordenarProductoSuministradorRepositorio;
        }

        // OBTENER LISTADO 

        public async Task<IEnumerable<CompraProductoSuministradorModel>> ObtenerListadoCompraProductoSuministradorAsync()
        {
            return await _context.CompraProductoSuministradorFacturacion.Include(o => o.OrdenAdquisicion).ThenInclude(p => p.ProductoSuministrador).ThenInclude(s => s.Suministrador).ToListAsync();
        }

        // OBTENER POR ID 

        public async Task<CompraProductoSuministradorModel> ObtenerListadoCompraProductoSuministradorPorIdAsync(int id)
        {
            return await _context.CompraProductoSuministradorFacturacion.Where(c => c.Id == id)
                         .FirstOrDefaultAsync();
        }

        // COMPRAR 

        public async Task ComprarAsync(int ordenId, CompraProductoSuministradorModel compraProductoSuministrador)
        {

            // 1 - MOCK DATA 
            // 2 - Return RedirectToAction - Comprar Method - Form

            var orden = await _ordenarProductoSuministradorRepositorio.ObtenerOrdenProductoPorIdAsync(ordenId);
            if (orden != null)
            {
                orden.Estado = EstadoOrdenProductoSuministradorEnum.Comprado;
                _context.OrdenesAdquisicion.Update(orden);
                await _context.SaveChangesAsync();
            }

            var compra = await _context.CompraProductoSuministradorFacturacion.FirstOrDefaultAsync(c => c.OrdenAdquisicionId == ordenId);

            compra = new CompraProductoSuministradorModel()
            {
                CIF = "12345678A", 
                DireccionLinea1 = "Calle Falsa 123",
                DireccionLinea2 = "Avenida Algo",
                Ciudad = "Villaviciosa",
                CodigoPostal = "28000",
                CorreoElectronicoEmpresa = "restofibonacci@gmail.com", 
                EstadoDeCompra = EstadoDeCompraEnum.PendienteDePago, 
                FechaDeEmision = DateTime.Now,
                Localidad = "Ejemplo", 
                MetodoDePago = MetodoDePagoEnum.NoEspecificado,
                NombreLegalDeLaEmpresa = "Resto Fibonacci",
                OrdenAdquisicionId = ordenId, 
                PaginaWebEmpresa = "www.restofibonacci.com", 
                Provincia = "Madrid", 
                TerminosPago = TerminosPagoEnum.noEspecificado,
                TelefonoEmpresa = "123456789"
            };

            await _context.CompraProductoSuministradorFacturacion.AddAsync(compra);
            await _context.SaveChangesAsync();
        }

        // EDITAR 

        // ELIMINAR 

        // FACTURAR (PDF)

        // EXCEL 

        // CSV
    }
}
