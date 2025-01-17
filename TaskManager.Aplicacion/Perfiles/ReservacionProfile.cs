using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Perfiles
{
    public class ReservacionProfile : Profile
    {
        public ReservacionProfile()
        {
            CreateMap<ReservacionDTO, ReservacionModel>()
                .ForMember(dest => dest.Nombre,
                opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellidos,
                opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Telefono,
                opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.CorreoElectronico,
                opt => opt.MapFrom(src => src.CorreoElectronico))
                .ForMember(dest => dest.FechadeReservacion,
                opt => opt.MapFrom(src => src.FechadeReservacion))
                .ForMember(dest => dest.HoradeReservacion,
                opt => opt.MapFrom(src => src.HoradeReservacion));

            CreateMap<ReservacionModel, ReservacionDTO>()
                .ForMember(dest => dest.Nombre,
                opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellidos,
                opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Telefono,
                opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.CorreoElectronico,
                opt => opt.MapFrom(src => src.CorreoElectronico))
                .ForMember(dest => dest.FechadeReservacion,
                opt => opt.MapFrom(src => src.FechadeReservacion))
                .ForMember(dest => dest.HoradeReservacion,
                opt => opt.MapFrom(src => src.HoradeReservacion));



        }
    }
}
