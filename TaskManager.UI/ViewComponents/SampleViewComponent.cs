using Microsoft.AspNetCore.Mvc;

namespace TaskManager.UI.ViewComponents
{
    public class SampleViewComponent : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
