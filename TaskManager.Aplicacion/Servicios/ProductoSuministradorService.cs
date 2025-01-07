using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{
    public class ProductoSuministradorService
    {
        private readonly IMapper _mapper; 

        public ProductoSuministradorService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProductoSuministradorModel ConvertToModel(ProductoSuministradorDTO dto)
        {
            return _mapper.Map<ProductoSuministradorModel>(dto);
        }

        public ProductoSuministradorDTO ConvertToDTO(ProductoSuministradorModel model)
        {
            return _mapper.Map<ProductoSuministradorDTO>(model);
        }
    }
}
