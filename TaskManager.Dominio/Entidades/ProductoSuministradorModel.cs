using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades
{
    public class ProductoSuministradorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nombre Producto")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Descripción Producto")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Precio Producto")]
        public double PrecioProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Cantidad Producto en Venta")]
        public int CantidadProductoEnVenta { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Unidad Producto")]
        public UnidadesEnum UnidadProductoEnum { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Categoria Producto")]
        public CategoriaProductoSuministradorEnum CategoriaProductoSuministradorEnum { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Suministrador")]
        public int SuministradorId { get; set; }    
        public IEnumerable<SuministradorModel> Suministrador { get; set; }
    }
}
