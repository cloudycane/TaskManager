using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ProductoSuministradorRepositorio : IProductoSuministradorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ProductoSuministradorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO 

        public async Task<IEnumerable<ProductoSuministradorModel>> ObtenerListadoProductoSuministradorAsync()
        {
            return await _context.ProductosSuministradores.Include("Suministrador").ToListAsync();
        }

        // OBTENER POR ID
        
        public async Task<ProductoSuministradorModel> ObtenerProductoSuministradorPorIdAsync(int id)
        {
            return await _context.ProductosSuministradores.FindAsync(id);
        }

        // CREAR 

        public async Task<int> CrearProductoSuministradorAsync(ProductoSuministradorModel productoSuministrador)
        {
            await _context.ProductosSuministradores.AddAsync(productoSuministrador);
            await _context.SaveChangesAsync();
            return productoSuministrador.Id;
        }

        // ELIMINAR
        // 
        public async Task EliminarProductoSuministradorAsync (int id)
        {
            var productoSuministrador = await ObtenerProductoSuministradorPorIdAsync(id);

            if (productoSuministrador != null)
            {
                _context.ProductosSuministradores.Remove(productoSuministrador);
                await _context.SaveChangesAsync();
            }
        }

        // ACTUALIZAR

        public async Task ActualizarProductoSuministradorAsync(ProductoSuministradorModel productoSuministradormodel)
        {
            var productoSuministradorAnterior = await _context.ProductosSuministradores.FindAsync(productoSuministradormodel.Id);

            if (productoSuministradorAnterior != null)
            {
                productoSuministradorAnterior.NombreProducto = productoSuministradormodel.NombreProducto;
                productoSuministradorAnterior.DescripcionProducto = productoSuministradorAnterior.DescripcionProducto;
                productoSuministradorAnterior.PrecioProducto = productoSuministradorAnterior.PrecioProducto;
                productoSuministradorAnterior.CantidadProductoEnVenta = productoSuministradorAnterior.CantidadProductoEnVenta;
                productoSuministradorAnterior.UnidadProductoEnum = productoSuministradorAnterior.UnidadProductoEnum;
                productoSuministradorAnterior.SuministradorId = productoSuministradorAnterior.SuministradorId;
                productoSuministradorAnterior.CategoriaProductoSuministradorEnum = productoSuministradorAnterior.CategoriaProductoSuministradorEnum;
            }

            _context.ProductosSuministradores.Update(productoSuministradorAnterior);
            await _context.SaveChangesAsync();
        }
    }

}
