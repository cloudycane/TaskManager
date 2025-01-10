using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class InventarioMateriaPrimaController : Controller
    {
        private readonly IInventarioMateriaPrimaRepositorio _inventarioMateriaPrimaRepositorio;
        
        public InventarioMateriaPrimaController(IInventarioMateriaPrimaRepositorio inventarioMateriaPrimaRepositorio)
        {
            _inventarioMateriaPrimaRepositorio = inventarioMateriaPrimaRepositorio;
        }
        // GET: InventarioMateriaPrimaController
        public async Task<IActionResult> Index()
        {
            var inventarioModel = await _inventarioMateriaPrimaRepositorio.ObtenerListadoInventarioMateriaPrimaAsync();

            var viewModel = new ListadoInventarioMateriaPrimaViewModel()
            {
                ListadoInventarioMateriasPrimasViewModel = inventarioModel
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AñadirAlInventario(int compraId, InventarioMateriaPrimaModel model)
        {
            if (ModelState.IsValid)
            {
                await _inventarioMateriaPrimaRepositorio.AñadirAlInventarioAsync(compraId, model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
