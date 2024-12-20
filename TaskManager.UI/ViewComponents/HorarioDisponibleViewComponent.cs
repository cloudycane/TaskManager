using Microsoft.AspNetCore.Mvc;

namespace TaskManager.UI.ViewComponents
{
    public class HorarioDisponibleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }


    }
}
