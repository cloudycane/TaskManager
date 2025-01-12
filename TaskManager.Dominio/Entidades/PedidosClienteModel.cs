using System.ComponentModel.DataAnnotations;
using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades
{
    public class PedidosClienteModel
    {
        public int Id { get; set; }
        public int ProductosParaVenderId { get; set; }
        public List<ProductosParaVenderModel> Pedidos { get; set; } = new List<ProductosParaVenderModel>();
        public int CantidadPedido { get; set; }
        public double PrecioTotal { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre y Apellidos")]
        public string NombreCliente { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Dirección")]
        public string DireccionCliente { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Teléfono")]
        public string TelefonoCliente { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Correo Electrónico")]
        public string CorreElectronicoCliente { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Método De Pago")]
        public MetodoDePagoEnumCliente MetodoDePago { get; set; }
        public DateTime FechaDePedido { get; set; }

    }
}
