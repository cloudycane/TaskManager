using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.UI.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class InventarioMateriaPrimaController : Controller
    {
        // GET: InventarioMateriaPrimaController
        public ActionResult Index()
        {
            return View();
        }

    }
}
