using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ReservacionRepositorio : IReservacionRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservacionService _reservacionService;

        // INSTANCIAR 

        public ReservacionRepositorio(ApplicationDbContext context, ReservacionService reservacionService)
        {
            _context = context;
            _reservacionService = reservacionService;
        }

        // OBTENER LISTADO (READ) 

        public async Task<IEnumerable<ReservacionModel>> ObtenerListadoReservacion()
        {
            return await _context.Reservaciones.ToListAsync();
        }

        // OBTENER POR ID

        public async Task<ReservacionModel> ObtenerReservacionPorId(int id)
        {
            return await _context.Reservaciones.FindAsync(id);
        }

        // CREAR 
        public async Task CrearReservacionAsync(ReservacionModel reservacion)
        {
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
        }

        // ELIMINAR 

        public async Task EliminarReservacion(int id)
        {
            var reservacion = await ObtenerReservacionPorId(id);

            if (reservacion != null)
            {
                _context.Reservaciones.Remove(reservacion);
                await _context.SaveChangesAsync();
            }
        }

        // EDITAR 

        public async Task EditarReservacion(ReservacionModel reservacionModel)
        {

            var reservacionAnterior = await ObtenerReservacionPorId(reservacionModel.Id);

            if (reservacionAnterior != null)
            {
                reservacionAnterior.Nombre = reservacionModel.Nombre;
                reservacionAnterior.Apellidos = reservacionModel.Apellidos;
                reservacionAnterior.Telefono = reservacionModel.Telefono;
                reservacionAnterior.CorreoElectronico = reservacionModel.CorreoElectronico;
                reservacionAnterior.FechadeReservacion = reservacionModel.FechadeReservacion;
                reservacionAnterior.HoradeReservacion = reservacionModel.HoradeReservacion;
                reservacionAnterior.EstadoReservacionEnum = reservacionModel.EstadoReservacionEnum;

                _context.Reservaciones.Update(reservacionAnterior);
                await _context.SaveChangesAsync();
            }

        }
        
    }
}
