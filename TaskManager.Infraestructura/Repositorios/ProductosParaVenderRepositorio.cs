using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ProductosParaVenderRepositorio : IProductosParaVenderRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ProductosParaVenderRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        // OBTENER LISTADO PRODUCTOS PARA VENDER 

      
         
        public async Task<IEnumerable<ProductosParaVenderModel>> ObtenerListadoProductosParaVenderAsync()
        {
            return await _context.ProductosParaVender.Include(i => i.Ingredientes).ToListAsync();
        }
 
         
    

        // OBTENER PRODUCTO POR ID 

        // CREAR PRODUCTO 

        public async Task CrearProductoParaVender(ProductosParaVenderModel productosParaVender)
        {
            await _context.ProductosParaVender.AddAsync(productosParaVender);
            await _context.SaveChangesAsync();
        }
    }
}
