using System.ComponentModel.DataAnnotations;
using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades
{
    public class PedidosClienteModel
    {
        public int Id { get; set; }
        public List<int> ProductosParaVenderIds { get; set; } = new List<int>();
        public List<int> CantidadPedido { get; set; } = new List<int>();
        public List<ProductosParaVenderModel> Pedidos { get; set; } = new List<ProductosParaVenderModel>();
        public double PrecioTotal { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string DireccionCliente { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string TelefonoCliente { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string CorreElectronicoCliente { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public MetodoDePagoEnumCliente MetodoDePago { get; set; }
        public EstadoDePedidoEnum EstadoPedido { get; set; }
        public DateTime FechaDePedido { get; set; }
        public List<PedidoProductoModel> PedidoProductos { get; set; } = new List<PedidoProductoModel>();

    }
}
