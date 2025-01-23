using AutoMapper;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades.DTOs;
using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Servicios
{
    public class PedidosClienteService : IPedidosClienteService
    {
        private readonly IMapper _mapper;

        public PedidosClienteService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PedidosClienteModel ConvertToModel(PedidosClienteDTO dto)
        {
            return _mapper.Map<PedidosClienteModel>(dto);
        }

        public PedidosClienteDTO ConvertToDTO(PedidosClienteModel model)
        {
            return _mapper.Map<PedidosClienteDTO>(model);
           
        }
    }
}
