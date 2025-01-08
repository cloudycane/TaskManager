using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{
    public class ComprarProductoSuministradorService
    {
        private readonly IMapper _mapper;

        public ComprarProductoSuministradorService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CompraProductoSuministradorModel ConvertToModel(CompraProductoSuministradorDTO dto)
        {
            return _mapper.Map<CompraProductoSuministradorModel>(dto);
        }

        public CompraProductoSuministradorDTO ConvertToDTO(CompraProductoSuministradorModel model)
        {
            return _mapper.Map<CompraProductoSuministradorDTO>(model);
        }
    }
}
