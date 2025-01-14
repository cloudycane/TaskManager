using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace TaskManager.UI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class GraficoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string graficoInicial()
        {
            Bitmap oBitmap = new Bitmap(800, 400);
            Graphics grafico = Graphics.FromImage(oBitmap);
            Rectangle rec;
            using (MemoryStream ms = new MemoryStream())
            {
                rec = new Rectangle(0, 50, 800, 350);

                grafico.FillRectangle(Brushes.Red, rec);
                rec = new Rectangle(0, 0, 800, 50);

                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(100, 100, 100, 50);

                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(600, 100, 100, 50);

                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(300, 200, 200, 50);

                grafico.FillRectangle(Brushes.White, rec);

                StringFormat formato = new StringFormat(StringFormatFlags.NoClip);

                formato.Alignment = StringAlignment.Center;

                grafico.DrawString("Mi Primer Grafico", new Font("Arial", 14), Brushes.Black, new Point(400, 0), formato);

                oBitmap.Save(ms, ImageFormat.Png);

                byte[] data = ms.ToArray();

                return Convert.ToBase64String(data);
            }
        }

    }
}
