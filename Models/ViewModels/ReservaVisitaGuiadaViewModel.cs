using System;
using System.ComponentModel.DataAnnotations;

namespace AppMuseo.Models.ViewModels
{
    public class ReservaVisitaGuiadaViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de la visita")]
        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "El horario es obligatorio")]
        [Display(Name = "Horario preferido")]
        public string Horario { get; set; }

        [Required(ErrorMessage = "El tipo de visita es obligatorio")]
        [Display(Name = "Tipo de visita")]
        public string TipoVisita { get; set; }

        [Display(Name = "Número de personas")]
        [Range(1, 20, ErrorMessage = "El número de personas debe estar entre 1 y 20")]
        public int NumeroPersonas { get; set; } = 1;

        [Display(Name = "¿Necesita algún servicio especial?")]
        public string NecesidadesEspeciales { get; set; }

        [Display(Name = "Comentarios adicionales")]
        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }

        // Propiedades para los tipos de visita disponibles
        public string[] TiposVisita => new[] { "Visita General", "Visita Temática", "Visita Nocturna", "Visita Privada" };
        
        // Propiedades para los horarios disponibles
        public string[] HorariosDisponibles => new[] { "11:00", "13:00", "17:00", "20:00 (solo viernes y sábados)" };
    }
}
