using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{
    public class ProductoSuministradorProfile : Profile
    {
        public ProductoSuministradorProfile()
        {
            CreateMap<ProductoSuministradorDTO, ProductoSuministradorModel>()
                .ForMember(dest => dest.NombreProducto,
                opt => opt.MapFrom(src => src.NombreProducto))
                .ForMember(dest => dest.SuministradorId,
                opt => opt.MapFrom(src => src.SuministradorId))
                .ForMember(dest => dest.PrecioProducto,
                opt => opt.MapFrom(src => src.PrecioProducto))
                .ForMember(dest => dest.CantidadProductoEnVenta,
                opt => opt.MapFrom(src => src.CantidadProductoEnVenta))
                .ForMember(dest => dest.CategoriaProductoSuministradorEnum,
                opt => opt.MapFrom(src => src.CategoriaProductoSuministradorEnum))
                .ForMember(dest => dest.Suministrador,
                opt => opt.MapFrom(src => src.Suministrador))
                .ForMember(dest => dest.UnidadProductoEnum,
                opt => opt.MapFrom(src => src.UnidadProductoEnum));

            CreateMap<ProductoSuministradorModel, ProductoSuministradorDTO>()
                .ForMember(dest => dest.NombreProducto,
                opt => opt.MapFrom(src => src.NombreProducto))
                .ForMember(dest => dest.SuministradorId,
                opt => opt.MapFrom(src => src.SuministradorId))
                .ForMember(dest => dest.PrecioProducto,
                opt => opt.MapFrom(src => src.PrecioProducto))
                .ForMember(dest => dest.CantidadProductoEnVenta,
                opt => opt.MapFrom(src => src.CantidadProductoEnVenta))
                .ForMember(dest => dest.CategoriaProductoSuministradorEnum,
                opt => opt.MapFrom(src => src.CategoriaProductoSuministradorEnum))
                .ForMember(dest => dest.Suministrador,
                opt => opt.MapFrom(src => src.Suministrador))
                .ForMember(dest => dest.UnidadProductoEnum,
                opt => opt.MapFrom(src => src.UnidadProductoEnum));
        }
    }
}
