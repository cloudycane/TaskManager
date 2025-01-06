using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{
    public class SuministradorService
    {
        private readonly IMapper _mapper;
        public SuministradorService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public SuministradorModel ConvertToModel(SuministradorDTO dto)
        {
            return _mapper.Map<SuministradorModel>(dto);
        }

        public SuministradorDTO ConvertToDTO(SuministradorModel model)
        {
            return _mapper.Map<SuministradorDTO>(model);
        }
    }
}
