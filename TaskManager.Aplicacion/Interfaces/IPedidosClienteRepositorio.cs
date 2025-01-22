using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IPedidosClienteRepositorio
    {
        Task CrearPedidosCliente(PedidosClienteModel model);
        Task<List<PedidosClienteModel>> ObtenerListadoPedidosClientesAsync();
        //Task<List<PedidosClienteModel>> ObtenerListadoPedidosClientesAsync();
        //Task<IEnumerable<PedidosClienteModel>> ObtenerListadoPedidosClientesAsync();
    }
}
