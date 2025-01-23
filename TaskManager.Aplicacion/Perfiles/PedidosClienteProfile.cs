using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Perfiles
{
    public class PedidosClienteProfile : Profile
    {
        public PedidosClienteProfile()
        {
            // Map from Model to DTO
            CreateMap<PedidosClienteModel, PedidosClienteDTO>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.NombreCliente))
                .ForMember(dest => dest.DireccionCliente, opt => opt.MapFrom(src => src.DireccionCliente))
                .ForMember(dest => dest.TelefonoCliente, opt => opt.MapFrom(src => src.TelefonoCliente))
                .ForMember(dest => dest.CorreElectronicoCliente, opt => opt.MapFrom(src => src.CorreElectronicoCliente))
                .ForMember(dest => dest.MetodoDePago, opt => opt.MapFrom(src => src.MetodoDePago))
                .ForMember(dest => dest.SelectedProductosParaVenderIds, opt => opt.MapFrom(src => src.ProductosParaVenderIds))
                .ForMember(dest => dest.CantidadesDeProductosParaVenderIds, opt => opt.MapFrom(src => src.CantidadPedido))
                .ForMember(dest => dest.PrecioDeLosProductosParaVenderIds, opt => opt.MapFrom(src => src.PedidoProductos.Select(p => p.ProductoParaVender.Precio)))
                .ForMember(dest => dest.ProductosParaVender, opt => opt.MapFrom(src => src.Pedidos));

            // Map from DTO to Model
            CreateMap<PedidosClienteDTO, PedidosClienteModel>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.NombreCliente))
                .ForMember(dest => dest.DireccionCliente, opt => opt.MapFrom(src => src.DireccionCliente))
                .ForMember(dest => dest.TelefonoCliente, opt => opt.MapFrom(src => src.TelefonoCliente))
                .ForMember(dest => dest.CorreElectronicoCliente, opt => opt.MapFrom(src => src.CorreElectronicoCliente))
                .ForMember(dest => dest.MetodoDePago, opt => opt.MapFrom(src => src.MetodoDePago))
                .ForMember(dest => dest.ProductosParaVenderIds, opt => opt.MapFrom(src => src.SelectedProductosParaVenderIds))
                .ForMember(dest => dest.CantidadPedido, opt => opt.MapFrom(src => src.CantidadesDeProductosParaVenderIds))
                .ForMember(dest => dest.PedidoProductos, opt => opt.Ignore()) 
                .ForMember(dest => dest.Pedidos, opt => opt.Ignore()); 
        }
    }
}
