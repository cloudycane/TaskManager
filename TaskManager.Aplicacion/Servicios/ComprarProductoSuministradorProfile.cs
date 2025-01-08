using AutoMapper;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Servicios
{
    public class ComprarProductoSuministradorProfile : Profile
    {
        public ComprarProductoSuministradorProfile()
        {
            CreateMap<CompraProductoSuministradorDTO, CompraProductoSuministradorModel>()
                .ForMember(dest => dest.FacturacionID, opt => opt.MapFrom(src => src.FacturacionID))
                .ForMember(dest => dest.OrdenAdquisicionId, opt => opt.MapFrom(src => src.OrdenAdquisicionId))
                .ForMember(dest => dest.OrdenAdquisicion, opt => opt.MapFrom(src => src.OrdenAdquisicion))
                .ForMember(dest => dest.Ordenes, opt => opt.MapFrom(src => src.Ordenes))
                .ForMember(dest => dest.PaginaWebEmpresa, opt => opt.MapFrom(src => src.PaginaWebEmpresa))
                .ForMember(dest => dest.CorreoElectronicoEmpresa, opt => opt.MapFrom(src => src.CorreoElectronicoEmpresa))
                .ForMember(dest => dest.TelefonoEmpresa, opt => opt.MapFrom(src => src.TelefonoEmpresa))
                .ForMember(dest => dest.NombreLegalDeLaEmpresa, opt => opt.MapFrom(src => src.NombreLegalDeLaEmpresa))
                .ForMember(dest => dest.CIF, opt => opt.MapFrom(src => src.CIF))
                .ForMember(dest => dest.DireccionLinea1, opt => opt.MapFrom(src => src.DireccionLinea1))
                .ForMember(dest => dest.DireccionLinea2, opt => opt.MapFrom(src => src.DireccionLinea2))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Localidad))
                .ForMember(dest => dest.CodigoPostal, opt => opt.MapFrom(src => src.CodigoPostal))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.EstadoDeCompra, opt => opt.MapFrom(src => src.EstadoDeCompra))
                .ForMember(dest => dest.MetodoDePago, opt => opt.MapFrom(src => src.MetodoDePago))
                .ForMember(dest => dest.FechaDeEmision, opt => opt.MapFrom(src => src.FechaDeEmision))
                .ForMember(dest => dest.TasaDeIva, opt => opt.MapFrom(src => src.TasaDeIva))
                .ForMember(dest => dest.TasaDeTransporte, opt => opt.MapFrom(src => src.TasaDeTransporte))
                .ForMember(dest => dest.TasaDeDescuento, opt => opt.MapFrom(src => src.TasaDeDescuento))
                .ForMember(dest => dest.GastosDeMantenimiento, opt => opt.MapFrom(src => src.GastosDeMantenimiento))
                .ForMember(dest => dest.GastosDeEnvio, opt => opt.MapFrom(src => src.GastosDeEnvio))
                .ForMember(dest => dest.TerminosPago, opt => opt.MapFrom(src => src.TerminosPago));

            CreateMap<CompraProductoSuministradorModel, CompraProductoSuministradorDTO>()
                .ForMember(dest => dest.FacturacionID, opt => opt.MapFrom(src => src.FacturacionID))
                .ForMember(dest => dest.OrdenAdquisicionId, opt => opt.MapFrom(src => src.OrdenAdquisicionId))
                .ForMember(dest => dest.OrdenAdquisicion, opt => opt.MapFrom(src => src.OrdenAdquisicion))
                .ForMember(dest => dest.Ordenes, opt => opt.MapFrom(src => src.Ordenes))
                .ForMember(dest => dest.PaginaWebEmpresa, opt => opt.MapFrom(src => src.PaginaWebEmpresa))
                .ForMember(dest => dest.CorreoElectronicoEmpresa, opt => opt.MapFrom(src => src.CorreoElectronicoEmpresa))
                .ForMember(dest => dest.TelefonoEmpresa, opt => opt.MapFrom(src => src.TelefonoEmpresa))
                .ForMember(dest => dest.NombreLegalDeLaEmpresa, opt => opt.MapFrom(src => src.NombreLegalDeLaEmpresa))
                .ForMember(dest => dest.CIF, opt => opt.MapFrom(src => src.CIF))
                .ForMember(dest => dest.DireccionLinea1, opt => opt.MapFrom(src => src.DireccionLinea1))
                .ForMember(dest => dest.DireccionLinea2, opt => opt.MapFrom(src => src.DireccionLinea2))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Localidad))
                .ForMember(dest => dest.CodigoPostal, opt => opt.MapFrom(src => src.CodigoPostal))
                .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.Ciudad))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.EstadoDeCompra, opt => opt.MapFrom(src => src.EstadoDeCompra))
                .ForMember(dest => dest.MetodoDePago, opt => opt.MapFrom(src => src.MetodoDePago))
                .ForMember(dest => dest.FechaDeEmision, opt => opt.MapFrom(src => src.FechaDeEmision))
                .ForMember(dest => dest.TasaDeIva, opt => opt.MapFrom(src => src.TasaDeIva))
                .ForMember(dest => dest.TasaDeTransporte, opt => opt.MapFrom(src => src.TasaDeTransporte))
                .ForMember(dest => dest.TasaDeDescuento, opt => opt.MapFrom(src => src.TasaDeDescuento))
                .ForMember(dest => dest.GastosDeMantenimiento, opt => opt.MapFrom(src => src.GastosDeMantenimiento))
                .ForMember(dest => dest.GastosDeEnvio, opt => opt.MapFrom(src => src.GastosDeEnvio))
                .ForMember(dest => dest.TerminosPago, opt => opt.MapFrom(src => src.TerminosPago));
        }
    }
    
}
