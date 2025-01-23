using AutoMapper;
using TaskManager.Dominio.Entidades.DTOs;
using TaskManager.Dominio.Entidades;

public class OrdenAdquisicionProfile : Profile
{
    public OrdenAdquisicionProfile()
    {
        // Map DTO to Model
        CreateMap<OrdenAdquisicionDTO, OrdenAdquisicionModel>()
            .ForMember(dest => dest.FechaDeCreacion, opt => opt.MapFrom(src => src.FechaDeCreacion))
            .ForMember(dest => dest.HoraDeCreacion, opt => opt.MapFrom(src => src.HoraDeCreacion))
            .ForMember(dest => dest.CantidadDeOrden, opt => opt.MapFrom(src => src.CantidadDeOrden))
            .ForMember(dest => dest.PrecioTotal, opt => opt.MapFrom(src => src.PrecioTotal))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos))
            .ForMember(dest => dest.ProductoSuministrador, opt => opt.MapFrom(src => src.ProductoSuministrador))
            .AfterMap((src, dest) =>
            {
                // Handle nested mapping explicitly if needed
                if (src.ProductoSuministrador != null)
                {
                    dest.ProductoSuministrador.NombreProducto = src.ProductoSuministrador.NombreProducto;

                    if (src.ProductoSuministrador.Suministrador != null)
                    {
                        dest.ProductoSuministrador.Suministrador.RazonSocial = src.ProductoSuministrador.Suministrador.RazonSocial;
                    }
                }
            });

        // Map Model to DTO
        CreateMap<OrdenAdquisicionModel, OrdenAdquisicionDTO>()
            .ForMember(dest => dest.FechaDeCreacion, opt => opt.MapFrom(src => src.FechaDeCreacion))
            .ForMember(dest => dest.HoraDeCreacion, opt => opt.MapFrom(src => src.HoraDeCreacion))
            .ForMember(dest => dest.CantidadDeOrden, opt => opt.MapFrom(src => src.CantidadDeOrden))
            .ForMember(dest => dest.PrecioTotal, opt => opt.MapFrom(src => src.PrecioTotal))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos))
            .ForMember(dest => dest.ProductoSuministrador, opt => opt.MapFrom(src => src.ProductoSuministrador))
            .AfterMap((src, dest) =>
            {
                // Handle nested mapping explicitly if needed
                if (src.ProductoSuministrador != null)
                {
                    dest.ProductoSuministrador.NombreProducto = src.ProductoSuministrador.NombreProducto;

                    if (src.ProductoSuministrador.Suministrador != null)
                    {
                        dest.ProductoSuministrador.Suministrador.RazonSocial = src.ProductoSuministrador.Suministrador.RazonSocial;
                    }
                }
            });
    }
}
