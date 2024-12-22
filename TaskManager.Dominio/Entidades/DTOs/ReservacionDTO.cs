
using System.ComponentModel.DataAnnotations;
using TaskManager.Dominio.Enum;

namespace TaskManager.Dominio.Entidades.DTOs
{
    public class ReservacionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(50, ErrorMessage = "{0} tiene un límite de {1} carácteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Phone(ErrorMessage = "Por favor, proporcionar un número de télefono válido.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "Por favor, proporcionar un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Fecha de Reservación")]
        public DateTime FechadeReservacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Hora de Reservación")]
        [Range(typeof(TimeSpan), "08:00", "22:00", ErrorMessage = "La hora de reservación debe estar entre 08:00 y 22:00.")]
        public TimeSpan HoradeReservacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Estado de Reservación")]
        public EstadoReservacionEnum EstadoReservacionEnum { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public MesasEnum MesasEnum { get; set; }
    }

}
