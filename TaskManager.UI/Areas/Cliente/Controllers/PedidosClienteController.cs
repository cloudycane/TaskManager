using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Enum;
using TaskManager.Infraestructura.Data;
using TaskManager.Infraestructura.Repositorios;

namespace TaskManager.UI.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class PedidosClienteController : Controller
    {
        private readonly IProductosParaVenderRepositorio _productosParaVenderRepositorio;
        private readonly IPedidosClienteRepositorio _clienteRepositorio;
        private readonly ApplicationDbContext _context;

        public PedidosClienteController(IProductosParaVenderRepositorio productosParaVenderRepositorio, IPedidosClienteRepositorio pedidosClienteRepositorio, ApplicationDbContext context)
        {
            _productosParaVenderRepositorio = productosParaVenderRepositorio;
            _clienteRepositorio = pedidosClienteRepositorio;
            _context = context;
        }

        public async Task<IActionResult> Crear()
        {
            var productosEnVentas = await _productosParaVenderRepositorio.ObtenerListadoProductosParaVenderAsync();
            ViewBag.ProductosEnVentas = productosEnVentas;
            return View(new PedidosClienteModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidosClienteModel model, int[] productosIds)
        {
            if (ModelState.IsValid)
            {
                foreach (var productoId in productosIds)
                {
                    var producto = await _context.ProductosParaVender.FindAsync(productoId);
                    if (producto != null)
                    {
                        model.Pedidos.Add(producto);
                    }
                }
                await _clienteRepositorio.CrearPedidosCliente(model);
                return RedirectToAction("Index");
            }
            var productosEnVentas = await _productosParaVenderRepositorio.ObtenerListadoProductosParaVenderAsync();
            ViewBag.ProductosEnVentas = productosEnVentas;
       
            return View(model);
        }
    }
}
