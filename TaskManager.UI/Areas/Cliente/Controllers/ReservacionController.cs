using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class ReservacionController : Controller
    {
        
        private readonly IReservacionRepositorio _reservacionRepositorio;
        private readonly ReservacionService _reservacionService;

        public ReservacionController(IReservacionRepositorio reservacionRepositorio, ReservacionService reservacionService)
        {
            _reservacionRepositorio = reservacionRepositorio;
            _reservacionService = reservacionService;
        }

        public async Task<IActionResult> Index()
        {
            var listadoReservaciones = await _reservacionRepositorio.ObtenerListadoReservacion();
            var viewModel = new ListadoReservacionesViewModel
            {
                ListadoReservaciones = listadoReservaciones
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ReservacionModel reservacion)
        {
            if (!ModelState.IsValid)
            {
                return View(reservacion);
            }

            await _reservacionRepositorio.CrearReservacionAsync(reservacion);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var reservacion = await _reservacionRepositorio.ObtenerReservacionPorId(id);

            if (reservacion == null)
            {
                return NotFound();
            }
            var dtoReservacion = _reservacionService.ConvertToDTO(reservacion);
            
            return View(dtoReservacion);
        }


        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmacion(int id)
        {

            var reservacion = await _reservacionRepositorio.ObtenerReservacionPorId(id);
            if (reservacion == null)
            {
                return NotFound();
            }
            await _reservacionRepositorio.EliminarReservacion(id);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var reservacion = await _reservacionRepositorio.ObtenerReservacionPorId(id);

            if (reservacion == null)
            {
                return NotFound();
            }

            var dtoReservacion = _reservacionService.ConvertToDTO(reservacion);
            
            return View(dtoReservacion);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Editar(ReservacionModel model)
        {
            if (!ModelState.IsValid)
            {
                var dto = _reservacionService.ConvertToDTO(model);
                return View(dto); 
            }

            await _reservacionRepositorio.EditarReservacion(model);
            return RedirectToAction("Index");
        }
    }
}
