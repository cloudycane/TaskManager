using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface ICompraProductoSuministradorFacturacionRepositorio
    {
        Task ComprarAsync(int ordenId, CompraProductoSuministradorModel compraProductoSuministrador);
        Task<IEnumerable<CompraProductoSuministradorModel>> ObtenerListadoCompraProductoSuministradorAsync();
        Task<CompraProductoSuministradorModel> ObtenerListadoCompraProductoSuministradorPorIdAsync(int id);
    }
}
