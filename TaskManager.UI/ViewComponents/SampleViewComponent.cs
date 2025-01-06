using Microsoft.AspNetCore.Mvc;
using TaskManager.Aplicacion.Interfaces;

namespace TaskManager.UI.ViewComponents
{
    public class SampleViewComponent : ViewComponent
    {
        private readonly IReservacionRepositorio _reservacionRepositorio;

        public SampleViewComponent(IReservacionRepositorio reservacionRepositorio)
		{
			_reservacionRepositorio = reservacionRepositorio;
		}
		public async Task<IViewComponentResult> InvokeAsync()
        {
			return View();
        }
    }
}
