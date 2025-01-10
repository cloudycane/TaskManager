using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades
{
    public class InventarioMateriaPrimaModel
    {
        public int Id { get; set; }
        public int CompraProductoSuministradorId { get; set; }
        public CompraProductoSuministradorModel CompraProductoSuministrador { get; set; }
        public CategoriaProductoInventarioEnum CategoriaProductoInventario { get; set; }
        public DateTime FechaDistribucion { get; set; }
        public EstadoProductoInventarioEnum EstadoProductoInventario { get; set; }
        public int CantidadProductoEnInventario { get; set; }

    }
}
