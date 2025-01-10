using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IProductosParaVenderRepositorio
    {
        Task CrearProductoParaVender(ProductosParaVenderModel productosParaVender);
        Task<IEnumerable<ProductosParaVenderModel>> ObtenerListadoProductosParaVenderAsync();
        // Task<IEnumerable<ProductosParaVenderModel>> ObtenerListadoProductosParaVenderAsync();
    }
}
