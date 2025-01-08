using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Index()
        {
            var compras = await _compraProductoSuministradorFacturacionRepositorio.ObtenerListadoCompraProductoSuministradorAsync();

            var viewModel = new ListadoComprasProductoSuministradorViewModel()
            {
                ComprasProductoSuministrador = compras
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
    }
}
