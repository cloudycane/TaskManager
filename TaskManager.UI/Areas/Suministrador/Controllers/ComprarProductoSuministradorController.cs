using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Enum;
using TaskManager.Infraestructura.Data;
using TaskManager.Infraestructura.Repositorios;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Suministrador.Controllers
{
    [Area("Suministrador")]
    public class ComprarProductoSuministradorController : Controller
    {
        private readonly ComprarProductoSuministradorService _comprarProductoSuministradorService;
        private readonly ICompraProductoSuministradorFacturacionRepositorio _compraProductoSuministradorFacturacionRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly IUnitofWork _unitOfWork;

        public ComprarProductoSuministradorController(ComprarProductoSuministradorService comprarProductoSuministradorService, ICompraProductoSuministradorFacturacionRepositorio compraProductoSuministradorFacturacionRepositorio, IUnitofWork unitOfWork, ApplicationDbContext context)
        {
            _compraProductoSuministradorFacturacionRepositorio = compraProductoSuministradorFacturacionRepositorio;
            _comprarProductoSuministradorService = comprarProductoSuministradorService;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4)
        {
            var compras = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorAsync();

            var paginatedList = compras.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new ListadoComprasProductoSuministradorViewModel()
            {
                ComprasProductoSuministrador = paginatedList,
                PaginaActual = pageNumber, 
                PaginasTotal = (int)Math.Ceiling(compras.Count() / (double)pageSize)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Comprar(int id)
        {
            var compra = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorPorIdAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            var dtoCompra = _comprarProductoSuministradorService.ConvertToDTO(compra);
            return View(dtoCompra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comprar(int compraId, CompraProductoSuministradorModel model)
        {
            if (ModelState.IsValid)
            {
                await _compraProductoSuministradorFacturacionRepositorio.ComprarAsync(compraId, model);
                return RedirectToAction("Index"); 
            }

            return View(model);
        }

        public async Task<IActionResult> CambiarTerminoPago(int id, TerminosPagoEnum terminoPago)
        {
            var compra = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorPorIdAsync(id);

            if (compra != null)
            {
                compra.TerminosPago = terminoPago; 
                _context.CompraProductoSuministradorFacturacion.Update(compra); 
                await _unitOfWork.GuardarAsync();
                return RedirectToAction("Index"); 
            }

            return NotFound();
        }

        public async Task<IActionResult> CambiarMetodoPago(int id, MetodoDePagoEnum metodoPago)
        {

            var compra = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorPorIdAsync(id);

            if (compra != null)
            {
                compra.MetodoDePago = metodoPago;
                _context.CompraProductoSuministradorFacturacion.Update(compra);
                await _unitOfWork.GuardarAsync();
                return RedirectToAction("Index"); 
            }

            return NotFound();

        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var compra = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorPorIdAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            var compraDTO = _comprarProductoSuministradorService.ConvertToDTO(compra);
            return View(compraDTO);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EliminarConfirmacion(int id)
        {
            var compra = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorPorIdAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            await _compraProductoSuministradorFacturacionRepositorio.EliminarCompra(id);
            TempData["SuccessMessage"] = "Enhorabuena, ha sido eliminada exitosamente";
            return RedirectToAction("Index");  

        }

        public async Task<IActionResult> ComprasProductosSuministradoresCSV()
        {
            MemoryStream memoryStream = await _compraProductoSuministradorFacturacionRepositorio.ObtenerCompraProductoCsv();
            return File(memoryStream, "application/octet-stream", "ComprasProductosSuministrador.csv");
        }

        public async Task<IActionResult> ComprasProductosSuministradoresExcel()
        {
            MemoryStream memoryStream = await _compraProductoSuministradorFacturacionRepositorio.ObtenerComprasProductosSuministradoresExcel();
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheet", "ComprasProductosSuministrador.csv");
        }
    }
}
