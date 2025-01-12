using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IPedidosClienteRepositorio
    {
        Task CrearPedidosCliente(PedidosClienteModel model);
    }
}
