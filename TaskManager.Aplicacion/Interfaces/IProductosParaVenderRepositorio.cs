using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IProductosParaVenderRepositorio
    {
        Task CrearProductoParaVender(ProductosParaVenderModel productosParaVender);
        Task<IEnumerable<ProductosParaVenderModel>> ObtenerListadoProductosParaVenderAsync();
        Task<MemoryStream> ObtenerListadoProductosParaVenderExcel();
        Task<MemoryStream> ObtenerProductosParaVenderCsv();
        // Task<IEnumerable<ProductosParaVenderModel>> ObtenerListadoProductosParaVenderAsync();
    }
}
