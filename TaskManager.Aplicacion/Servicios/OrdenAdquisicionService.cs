using AutoMapper;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{

    public class OrdenAdquisicionService : IOrdenAdquisicionService
    {
        private readonly IMapper _mapper;
        private readonly IOrdenarProductoSuministradorRepositorio _ordenarProductoSuministradorRepositorio;

        public OrdenAdquisicionService(IMapper mapper, IOrdenarProductoSuministradorRepositorio ordenarProductoSuministradorRepositorio)
        {
            _mapper = mapper;
            _ordenarProductoSuministradorRepositorio = ordenarProductoSuministradorRepositorio;
        }

        public OrdenAdquisicionModel ConvertToModel (OrdenAdquisicionDTO dto)
        {
            return _mapper.Map<OrdenAdquisicionModel>(dto);
        }

        public OrdenAdquisicionDTO ConvertToDTO (OrdenAdquisicionModel model)
        {
            return _mapper.Map<OrdenAdquisicionDTO>(model);
        }
    }
}
