using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Repositorios;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Suministrador.Controllers
{
    [Area("Suministrador")]
    public class ProductoSuministradorController : Controller
    {
        private readonly ProductoSuministradorService _productoSuministradorService;
        private readonly IProductoSuministradorRepositorio _productoSuministradorRepositorio;
        private readonly ISuministradorRepositorio _suministradorRepositorio;

        public ProductoSuministradorController(ProductoSuministradorService productoSuministradorService, IProductoSuministradorRepositorio productoSuministradorRepositorio, ISuministradorRepositorio suministradorRepositorio)
        {
            _productoSuministradorService = productoSuministradorService;
            _productoSuministradorRepositorio = productoSuministradorRepositorio;
            _suministradorRepositorio = suministradorRepositorio;
        }
        public async Task<IActionResult> Index()
        {
            var productoSuministrador = await _productoSuministradorRepositorio.ObtenerListadoProductoSuministradorAsync();

            var viewModel = new ListadoProductosSuministradorViewModel
            {
                Productos = productoSuministrador
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var model = new ProductoSuministradorModel
            {
                Suministrador = await _suministradorRepositorio.ObtenerListadoSuministradorAsync()
            };
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Crear(ProductoSuministradorModel productoSuministrador)
        {
            if(!ModelState.IsValid)
            {
                return View(productoSuministrador);
            }

            await _productoSuministradorRepositorio.CrearProductoSuministradorAsync(productoSuministrador);
            TempData["SuccessMessage"] = "Producto creado exitosamente";
            return RedirectToAction("Index");

        }
    }
}
