using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;

namespace TaskManager.UI.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class ReservacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservacionRepositorio _reservacionRepositorio; 

        public ReservacionController(ApplicationDbContext context, IReservacionRepositorio reservacionRepositorio)
        {
            _context = context;
            _reservacionRepositorio = reservacionRepositorio;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ReservacionModel reservacion)
        {
            if (!ModelState.IsValid)
            {
                return View(reservacion);
            }

            await _reservacionRepositorio.CrearReservacionAsync(reservacion);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
