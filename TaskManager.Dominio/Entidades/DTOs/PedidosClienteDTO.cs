using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades.DTOs
{
    public class PedidosClienteDTO
    {
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

        [Display(Name = "Productos")]
        public List<int> SelectedProductosParaVenderIds { get; set; } = new List<int>();
        public List<int> CantidadesDeProductosParaVenderIds { get; set; } = new List<int>();
        public List<double> PrecioDeLosProductosParaVenderIds { get; set; } = new List<double>();  

        // This will hold the list of available products for display
        public List<ProductosParaVenderModel> ProductosParaVender { get; set; }
    }
}
