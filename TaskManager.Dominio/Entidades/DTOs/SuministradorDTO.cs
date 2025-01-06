using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dominio.Entidades.DTOs
{
    public class SuministradorDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        [Display(Name = "Dirección Línea 1")]
        public string DireccionLinea1 { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        [Display(Name = "Dirección Línea 2")]
        public string DireccionLinea2 { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        [Display(Name = "País")]
        public string Pais { get; set; }
    }
}
