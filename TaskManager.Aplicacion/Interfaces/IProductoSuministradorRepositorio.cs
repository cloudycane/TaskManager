using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IProductoSuministradorRepositorio
    {
        Task ActualizarProductoSuministradorAsync(ProductoSuministradorModel productoSuministradormodel);
        Task<int> CrearProductoSuministradorAsync(ProductoSuministradorModel productoSuministrador);
        Task EliminarProductoSuministradorAsync(int id);
        Task<IEnumerable<ProductoSuministradorModel>> ObtenerListadoProductoSuministradorAsync();
        Task<MemoryStream> ObtenerListadoProductoSuministradorExcel();
        Task<MemoryStream> ObtenerProductoSuministradorCsv();
        Task<ProductoSuministradorModel> ObtenerProductoSuministradorPorIdAsync(int id);
        Task OrdenarProducto(int productoId, int productoCantidad, OrdenAdquisicionModel ordenAdquisicion);
    }
}
