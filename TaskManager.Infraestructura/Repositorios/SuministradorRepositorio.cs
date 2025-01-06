using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class SuministradorRepositorio : ISuministradorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public SuministradorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        #region OBTENER LISTADO DE SUMINISTRADORES 

        public async Task<IEnumerable<SuministradorModel>> ObtenerListadoSuministradorAsync()
        {
            return await _context.Suministradores.ToListAsync();
        }

        #endregion

        #region OBTENER SUMINISTRADOR POR ID 

        public async Task<SuministradorModel> ObtenerSuministradorPorIdAsync(int id)
        {
            return await _context.Suministradores.FindAsync(id);
        }

        #endregion

        #region CREAR SUMINISTRADOR

        public async Task CrearSuministradorAsync(SuministradorModel suministrador)
        {
            await _context.Suministradores.AddAsync(suministrador);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region ELIMINAR SUMINISTRADOR

        public async Task EliminarSuministrador(int id)
        {
            var suministrador = await ObtenerSuministradorPorIdAsync(id);

            if (suministrador != null)
            {
                _context.Suministradores.Remove(suministrador);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region ACTUALIZAR SUMINISTRADOR
        
        public async Task ActualizarSuministrador(SuministradorModel suministrador)
        {
            var suministradorAnterior = await ObtenerSuministradorPorIdAsync(suministrador.Id);

            if (suministradorAnterior != null)
            {
                suministradorAnterior.RazonSocial = suministrador.RazonSocial;
                suministradorAnterior.DireccionLinea1 = suministrador.DireccionLinea1;
                suministradorAnterior.DireccionLinea2 = suministrador.DireccionLinea2;
                suministradorAnterior.Localidad = suministrador.Localidad;
                suministradorAnterior.Pais = suministrador.Pais;    
                suministradorAnterior.Provincia = suministrador.Provincia;
                await _context.SaveChangesAsync();
            }
        }
        
        #endregion

    }
}
