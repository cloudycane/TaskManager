using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;

namespace TaskManager.UI.Areas.Suministrador.Controllers
{
    [Area("Suministrador")]
    public class GestionarOrdenController : Controller
    {
        private readonly IOrdenarProductoSuministradorRepositorio _ordenarProductoSuministradorRepositorio;
        private readonly IOrdenAdquisicionService _ordenAdquisicionService;
        private readonly IProductoSuministradorRepositorio _productoSuministradorRepositorio;
        
        public GestionarOrdenController(IOrdenarProductoSuministradorRepositorio ordenarProductoSuministradorRepositorio, IOrdenAdquisicionService ordenAdquisicionService, IProductoSuministradorRepositorio productoSuministradorRepositorio)
        {
            _ordenarProductoSuministradorRepositorio = ordenarProductoSuministradorRepositorio;
            _ordenAdquisicionService = ordenAdquisicionService;
            _productoSuministradorRepositorio = productoSuministradorRepositorio;
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var ordenProducto = await _ordenarProductoSuministradorRepositorio.ObtenerOrdenProductoPorIdAsync(id);
            if (ordenProducto == null)
            {
                return NotFound();
            }
            var productos = await _productoSuministradorRepositorio.ObtenerListadoProductoSuministradorAsync();
            var ordenProductoSuministradorDTO = _ordenAdquisicionService.ConvertToDTO(ordenProducto);
            ordenProductoSuministradorDTO.Productos = productos;

            return View(ordenProductoSuministradorDTO);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EliminarConfirmacion(int id)
        {
            var ordenProducto = await _ordenarProductoSuministradorRepositorio.ObtenerOrdenProductoPorIdAsync(id);

            if (ordenProducto == null)
            {
                return NotFound();
            }

            await _ordenarProductoSuministradorRepositorio.EliminarOrdenProductoSuministrador(id);
            TempData["SuccessMessage"] = "Enhorabuena, ha sido eliminada exitosamente";
            return RedirectToAction("Ordenes", "ProductoSuministrador");
        }

    }
}
