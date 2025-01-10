using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Produccion.Controllers
{
    [Area("Produccion")]
    public class IngrendientesController : Controller
    {
        private readonly IIngrendientesRepositorio _ingrendientesRepositorio;
        
        public IngrendientesController(IIngrendientesRepositorio ingrendientesRepositorio)
        {
            _ingrendientesRepositorio = ingrendientesRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            var ingredientesListado = await _ingrendientesRepositorio.ObtenerListadoIngrendientesAsync();

            var viewModel = new ListadoIngrendientesViewModel()
            {
                Ingrendientes = ingredientesListado
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

        public async Task<IActionResult> Crear(IngrendientesModel model)
        {
            if (ModelState.IsValid)
            {
                await _ingrendientesRepositorio.CrearIngrendienteAsync(model);
                return RedirectToAction("Index");

            };

            return View(model);
        }

    }
}
