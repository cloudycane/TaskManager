namespace TaskManager.Dominio.Entidades
{
    public class InventarioMateriaPrimaModel
    {
        public int Id { get; set; }
        public int CompraProductoSuministradorId { get; set; }
        public CompraProductoSuministradorModel CompraProductoSuministrador { get; set; }

    }
}
