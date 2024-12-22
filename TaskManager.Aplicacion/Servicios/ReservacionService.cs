using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{
    public class ReservacionService
    {
        private readonly IMapper _mapper;
        
        public ReservacionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ReservacionModel ConvertToModel(ReservacionDTO dto)
        {
            return _mapper.Map<ReservacionModel>(dto);
        }

        public ReservacionDTO ConvertToDTO(ReservacionModel model)
        {
            return _mapper.Map<ReservacionDTO>(model);
        }
    }
}
