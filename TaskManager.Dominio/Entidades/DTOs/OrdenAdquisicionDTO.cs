using System.ComponentModel.DataAnnotations;
using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades.DTOs
{
    public class OrdenAdquisicionDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Producto Suministrador")]
        public int ProductoSuministradorId { get; set; }
        public ProductoSuministradorModel ProductoSuministrador { get; set; }
        public IEnumerable<ProductoSuministradorModel> Productos { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cantidad de Orden")]
        public int CantidadDeOrden { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Precio Total")]
        public double PrecioTotal { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado de Orden")]
        public EstadoOrdenProductoSuministradorEnum Estado { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public TimeSpan HoraDeCreacion { get; set; }
    }
}
