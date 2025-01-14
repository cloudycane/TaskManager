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
                foreach (var ingredienteId in ingredientesIds)
                {
                    var productoSuministrador = await _productoSuministradorRepositorio.ObtenerProductoSuministradorPorIdAsync(ingredienteId);
                    if (productoSuministrador != null)
                    {
                        productosParaVender.Ingredientes.Add(productoSuministrador);
                    }
                }

                // FILE UPLOAD 

                if (imagen != null && imagen.Length > 0)
                {
                    // JPG OR PNG ONLY 

                    var formatoPermitido = new[] { "image/jpeg", "image/png" };
                    if (!formatoPermitido.Contains(imagen.ContentType))
                    {
                        ModelState.AddModelError("imagen", "Formato no permitido");
                        return View(productosParaVender);
                    }

                    // Validate file size (max 2 MB)
                    if (imagen.Length > 2 * 1024 * 1024) // 2 MB
                    {
                        ModelState.AddModelError("imagen", "La imagen debe tener como máximo 2 MB.");
                        return View(productosParaVender);
                    }

                    // Set the directory path for saving the image
                    string folderPath = Path.Combine("wwwroot", "img", "imagen_productoVender");

                    // Create the directory if it doesn't exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Create a unique file name
                    string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imagen.FileName)}";
                    string filePath = Path.Combine(folderPath, fileName);

                    // Save the file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    // Save the relative file path in the model's property
                    productosParaVender.ImagePath = Path.Combine("img", "imagen_productoVender", fileName).Replace("\\", "/");
                }

                await _productosParaVenderRepositorio.CrearProductoParaVender(productosParaVender);
                return RedirectToAction("Index");

            }

            ViewBag.ProductosSuministradores = await _productoSuministradorRepositorio.ObtenerListadoProductoSuministradorAsync();
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

