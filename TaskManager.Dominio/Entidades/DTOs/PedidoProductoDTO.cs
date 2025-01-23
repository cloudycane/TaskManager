namespace TaskManager.Dominio.Entidades.DTOs
{
    public class PedidoProductoDTO
    {
        public int PedidoClienteId { get; set; }
        public PedidosClienteModel PedidoCliente { get; set; }
        public int ProductoParaVenderId { get; set; }
        public int CantidadDePedido { get; set; }
        public double PrecioTotal { get; set; }
        public ProductosParaVenderModel ProductoParaVender { get; set; }
    }
}
