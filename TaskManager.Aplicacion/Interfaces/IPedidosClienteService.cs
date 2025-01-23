using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IPedidosClienteService
    {
        PedidosClienteDTO ConvertToDTO(PedidosClienteModel model);
        PedidosClienteModel ConvertToModel(PedidosClienteDTO dto);
    }
}
