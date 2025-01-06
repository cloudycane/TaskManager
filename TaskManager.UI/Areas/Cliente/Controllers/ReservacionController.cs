using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
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
		public async Task<IActionResult> DetalleReservacionCliente(int id)
		{
			var reservacion = await _reservacionRepositorio.ObtenerReservacionPorId(id);
			if (reservacion == null)
			{
				return NotFound();
			}

			return View(reservacion);
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
            var reservacionId = await _reservacionRepositorio.CrearReservacionAsync(reservacion);
            TempData["SuccessMessage"] = "Enhorabuena, ha sido creada exitosamente";
			return RedirectToAction("DetalleReservacionCliente", new { id = reservacionId });
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
			TempData["SuccessMessage"] = "Enhorabuena, ha sido eliminada exitosamente";
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
			TempData["SuccessMessage"] = "Enhorabuena, ha sido editada exitosamente";
			return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReservacionPDF(int id)
		{
			var reservacion = await _reservacionRepositorio.ObtenerReservacionPorId(id);
			if (reservacion == null)
			{
				return NotFound();
			}
			var dtoReservacion = _reservacionService.ConvertToDTO(reservacion);
			return new ViewAsPdf("ReservacionPDF", dtoReservacion);
		}

        public async Task<IActionResult> ObtenerReservacionesCSV()
        {
            MemoryStream memoryStream = await _reservacionRepositorio.ObtenerReservacionCSV();
            return File(memoryStream, "application/octet-stream", "Reservaciones.csv");
        }

        public async Task<IActionResult> ObtenerReservacionesExcel()
        {
            MemoryStream memoryStream = await _reservacionRepositorio.ObtenerReservacionExcel();
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reservaciones.xlsx");
        }

    }
}