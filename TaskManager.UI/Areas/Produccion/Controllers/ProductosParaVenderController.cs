using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Repositorios;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.Areas.Produccion.Controllers
{
    [Area("Produccion")]
    public class ProductosParaVenderController : Controller
    {
        private readonly IProductosParaVenderRepositorio _productosParaVenderRepositorio;
        private readonly IIngrendientesRepositorio _ingrendientesRepositorio;
        private readonly IProductoSuministradorRepositorio _productoSuministradorRepositorio;
        public ProductosParaVenderController(IProductosParaVenderRepositorio productosParaVenderRepositorio, IIngrendientesRepositorio ingrendientesRepositorio, IProductoSuministradorRepositorio productoSuministradorRepositorio)
        {
            _productosParaVenderRepositorio = productosParaVenderRepositorio;
            _ingrendientesRepositorio = ingrendientesRepositorio;
            _productoSuministradorRepositorio = productoSuministradorRepositorio; 
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4)
        {
            var listadoProductoParaVender = await _productosParaVenderRepositorio.ObtenerListadoProductosParaVenderAsync();

            var paginatedList = listadoProductoParaVender.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new ListadoProductosParaVenderViewModel()
            {
                ListadoProductosParaVender = paginatedList, 
                PaginaActual = pageNumber, 
                PaginasTotal = (int)Math.Ceiling(listadoProductoParaVender.Count() / (double)pageSize)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var productoSuministradores = await _productoSuministradorRepositorio.ObtenerListadoProductoSuministradorAsync();
            ViewBag.ProductosSuministradores = productoSuministradores;
 
            return View(new ProductosParaVenderModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductosParaVenderModel productosParaVender, int[] ingredientesIds, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                // Initialize an empty list for ingredientes
                List<ProductoSuministradorModel> ingredientesSeleccionados = new List<ProductoSuministradorModel>();

                // Loop through each selected ingredient ID
                foreach (var ingredienteId in ingredientesIds)
                {
                    var productoSuministrador = await _productoSuministradorRepositorio.ObtenerProductoSuministradorPorIdAsync(ingredienteId);

                    if (productoSuministrador != null)
                    {
                        // Add each ingredient to the list
                        ingredientesSeleccionados.Add(productoSuministrador);
                    }
                }

                // Assign the ingredients list to the new product
                productosParaVender.Ingredientes = ingredientesSeleccionados;

                // Handle image upload
                if (imagen != null && imagen.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imagen.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }
                    productosParaVender.ImagePath = "/images/" + imagen.FileName;
                }

                // Save the new product to the database
                await _productosParaVenderRepositorio.CrearProductoParaVender(productosParaVender);

                // Redirect to the Index page or wherever you need
                return RedirectToAction("Index");
            }

            // If ModelState is invalid, return the same view with validation errors
            return View(productosParaVender);
        }


        public async Task<IActionResult> ObtenerProductosParaVenderCSV()
        {
            MemoryStream memoryStream = await _productosParaVenderRepositorio.ObtenerProductosParaVenderCsv();
            return File(memoryStream, "application/octet-stream", "ListadoProductosParaVender.csv");
        }
     
        public async Task<IActionResult> ObtenerProductosParaVenderExcel()
        {
            MemoryStream memoryStream = await _productosParaVenderRepositorio.ObtenerListadoProductosParaVenderExcel();
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheet", "LibroProductosParaVender.xlsx");
        }
        

    }
        
}

