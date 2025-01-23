using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;
using TaskManager.Infraestructura.Data;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class PedidosClienteController : Controller
    {
        private readonly IProductosParaVenderRepositorio _productosParaVenderRepositorio;
        private readonly IPedidosClienteRepositorio _clienteRepositorio;
        private readonly IPedidosClienteService _pedidosService;
        private readonly ApplicationDbContext _context;

        public PedidosClienteController(IProductosParaVenderRepositorio productosParaVenderRepositorio, IPedidosClienteRepositorio pedidosClienteRepositorio,IPedidosClienteService pedidosService ,ApplicationDbContext context)
        {
            _productosParaVenderRepositorio = productosParaVenderRepositorio;
            _clienteRepositorio = pedidosClienteRepositorio;
            _pedidosService = pedidosService;
            _context = context;
        }

        // GET: PedidosClientes/Index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4)
        {
            var pedidos = await _context.PedidosClientes
                .Include(p => p.PedidoProductos)
                    .ThenInclude(pp => pp.ProductoParaVender)
                .ToListAsync();

            var paginatedList = pedidos.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new ListadoPedidosClientesViewModel
            {
                PedidosClientes = paginatedList,
                PaginaActual = pageNumber,
                PaginasTotal = (int)Math.Ceiling(pedidos.Count() / (double)pageSize)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Crear()
        {
      
            var productosParaVender = await _context.ProductosParaVender.ToListAsync();

            var viewModel = new PedidosClienteDTO
            {
                ProductosParaVender = productosParaVender
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PedidosClienteDTO viewModel)
        {
            if (ModelState.IsValid)
            {
                var pedidoProductos = viewModel.SelectedProductosParaVenderIds
                                                .Zip(viewModel.CantidadesDeProductosParaVenderIds, (productoId, cantidad) => new PedidoProductoModel
                                                {
                                                ProductoParaVenderId = productoId,
                                                CantidadDePedido = cantidad
                                                }).ToList();


                var nuevoPedido = new PedidosClienteModel
                {
                    NombreCliente = viewModel.NombreCliente,
                    DireccionCliente = viewModel.DireccionCliente,
                    TelefonoCliente = viewModel.TelefonoCliente,
                    CorreElectronicoCliente = viewModel.CorreElectronicoCliente,
                    MetodoDePago = viewModel.MetodoDePago,
                    FechaDePedido = DateTime.Now,
                    PedidoProductos = pedidoProductos,
                    


                };

                _context.PedidosClientes.Add(nuevoPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.ProductosParaVender = await _context.ProductosParaVender.ToListAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> PedidosPDF(int id)
        {
            var pedidosCliente = await _context.PedidosClientes
                .Include(p => p.PedidoProductos) // Include related PedidoProductos
                .ThenInclude(pp => pp.ProductoParaVender) // Include related ProductoParaVender details
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedidosCliente == null)
            {
                return NotFound();
            }

            // Convert to DTO for passing to the view
            var dtoPedidosCliente = new PedidosClienteDTO
            {
                NombreCliente = pedidosCliente.NombreCliente,
                DireccionCliente = pedidosCliente.DireccionCliente,
                TelefonoCliente = pedidosCliente.TelefonoCliente,
                CorreElectronicoCliente = pedidosCliente.CorreElectronicoCliente,
                MetodoDePago = pedidosCliente.MetodoDePago,
                PedidoProductos = pedidosCliente.PedidoProductos.Select(pp => new PedidoProductoModel
                {
                    PedidoClienteId = pp.PedidoClienteId,
                    ProductoParaVenderId = pp.ProductoParaVenderId,
                    CantidadDePedido = pp.CantidadDePedido,
                    PrecioTotal = pp.PrecioTotal,
                    ProductoParaVender = pp.ProductoParaVender
                }).ToList()
            };

            return new ViewAsPdf("PedidosPDF", dtoPedidosCliente)
            {
                FileName = "Comprobante.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A6 // Adjust the page size
            };
        }

    }
}
