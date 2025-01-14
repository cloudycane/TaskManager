using Microsoft.AspNetCore.Mvc;

namespace TaskManager.UI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class InicioController : Controller
    {
        // GET: InicioController
        public ActionResult Index()
        {
            return View();
        }

        
    }
}
