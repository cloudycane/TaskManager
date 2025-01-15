using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Repositorios;
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

        public async Task<IActionResult> ObtenerListadoInventarioCSV()
        {
            MemoryStream memoryStream = await _inventarioMateriaPrimaRepositorio.ObtenerInventarioMateriaPrimaCSV();
            return File(memoryStream, "application/octet-stream", "InventarioMateriasPrimas.csv");
        }

        public async Task<IActionResult> ObtenerListadoInventarioExcel()
        {
            MemoryStream memoryStream = await _inventarioMateriaPrimaRepositorio.ObtenerInventarioMateriaPrimaExcel();
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InventarioMateriasPrimas.xlsx");
        }

    }
}
