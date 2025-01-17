using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Perfiles
{
    public class SuministradorProfile : Profile
    {
        public SuministradorProfile()
        {
            CreateMap<SuministradorDTO, SuministradorModel>()
                .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial))
                .ForMember(dest => dest.DireccionLinea1, opt => opt.MapFrom(src => src.DireccionLinea1))
                .ForMember(dest => dest.DireccionLinea2, opt => opt.MapFrom(src => src.DireccionLinea2))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Localidad))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais));


            CreateMap<SuministradorModel, SuministradorDTO>()
                .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial))
                .ForMember(dest => dest.DireccionLinea1, opt => opt.MapFrom(src => src.DireccionLinea1))
                .ForMember(dest => dest.DireccionLinea2, opt => opt.MapFrom(src => src.DireccionLinea2))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Localidad))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais));
        }
    }
}
