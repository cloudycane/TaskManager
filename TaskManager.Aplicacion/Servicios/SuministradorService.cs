using AutoMapper;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades.DTOs;
using TaskManager.Dominio.Entidades;

public class SuministradorService : ISuministradorService
{
    private readonly IMapper _mapper;
    private readonly ISuministradorRepositorio _suministradorRepositorio;
    private readonly ISuministradorRepositorio suministradorRepositorio;

    public SuministradorService(IMapper mapper, ISuministradorRepositorio suministradorRepositorio)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _suministradorRepositorio = suministradorRepositorio ?? throw new ArgumentNullException(nameof(suministradorRepositorio));
    }

    #region Convertir los modelos a DTO y vice versa.
    public SuministradorModel ConvertToModel(SuministradorDTO dto)
    {
        return _mapper.Map<SuministradorModel>(dto);
    }

    public SuministradorDTO ConvertToDTO(SuministradorModel model)
    {
        return _mapper.Map<SuministradorDTO>(model);
    }
    #endregion

    #region Método de servicio para obtener un listado de los suministradores
    public async Task<List<SuministradorModel>> ObtenerListadoDeLosSuministradoresAsync()
    {
        var listadoSuministradores = await _suministradorRepositorio.ObtenerListadoSuministradorAsync();
        return listadoSuministradores;
    }
    #endregion

    #region Método de servicio para obtener un suministrador por su Id 
    
    public async Task<SuministradorModel> ObtenerUnSuministradorPorIdAsync(int id)
    {
        var suministrador = await _suministradorRepositorio.ObtenerSuministradorPorIdAsync(id);
        return suministrador;
    }

    #endregion

    #region CREAR

    public async Task<SuministradorModel> CrearSuministradorAsync(SuministradorModel suministrador)
    {
       await _suministradorRepositorio.CrearSuministradorAsync(suministrador);
       var suministradorId = suministrador.Id;
       var suministradorCreado = await ObtenerUnSuministradorPorIdAsync(suministradorId);
       return suministradorCreado;
       
    }

    #endregion

    #region ELIMINAR 

    public async Task<bool> EliminarSuministradorModel(int id)
    {
        var suministrador = await ObtenerUnSuministradorPorIdAsync(id);

        if (suministrador == null)
        {
            return false;
        }

        await _suministradorRepositorio.EliminarSuministrador(id);
        return true;
    }


    #endregion
}
