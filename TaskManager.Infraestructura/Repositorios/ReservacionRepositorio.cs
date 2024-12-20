using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public class ReservacionRepositorio : IReservacionRepositorio
    {
        private readonly ApplicationDbContext _context; 

        // INSTANCIAR 

        public ReservacionRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }
        // CREAR 

        public async Task CrearReservacionAsync(ReservacionModel reservacion)
        {
            await _context.Reservaciones.AddAsync(reservacion);
            await _context.SaveChangesAsync();
        }
    }
}
