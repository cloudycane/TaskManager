using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades
{
    public class ProductosParaVenderModel
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int CantidadPedido { get; set; }
        public string ImagePath { get; set; }
        public DateTime FechaDeProduccion { get; set; }
        public CategoriaProductoEnVentasEnum CategoriaProductoEnVentas { get; set; }
        public List<PedidoProductoModel> PedidoProductos { get; set; } = new List<PedidoProductoModel>();


    }
}
