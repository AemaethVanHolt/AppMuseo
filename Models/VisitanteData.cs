using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AppMuseo.Models
{
    public class VisitanteData : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [Display(Name = "Apellidos")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden tener más de 100 caracteres")]
        public string Apellidos { get; set; } = string.Empty;

        [Display(Name = "Dirección")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "La ciudad no puede tener más de 100 caracteres")]
        public string Ciudad { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "La provincia no puede tener más de 100 caracteres")]
        public string Provincia { get; set; } = string.Empty;

        [Display(Name = "Código Postal")]
        [StringLength(10, ErrorMessage = "El código postal no puede tener más de 10 caracteres")]
        public string CodigoPostal { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El país no puede tener más de 100 caracteres")]
        public string Pais { get; set; } = string.Empty;

        [Display(Name = "Teléfono")]
        [StringLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [Display(Name = "Imagen de perfil")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Género")]
        [StringLength(20)]
        public string Genero { get; set; } = string.Empty;

        [Display(Name = "Biografía")]
        [StringLength(1000, ErrorMessage = "La biografía no puede tener más de 1000 caracteres")]
        public string Biografia { get; set; } = string.Empty;

        [Display(Name = "Sitio Web")]
        [Url(ErrorMessage = "Por favor, introduce una URL válida")]
        public string SitioWeb { get; set; } = string.Empty;

        [Display(Name = "Redes Sociales")]
        public string RedesSociales { get; set; } = string.Empty;

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display(Name = "Último Acceso")]
        public DateTime? UltimoAcceso { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        [Display(Name = "Preferencias de Notificación")]
        public bool RecibirNotificaciones { get; set; } = true;

        [Display(Name = "Preferencias de Privacidad")]
        public string PreferenciasPrivacidad { get; set; } = "público";

        [Display(Name = "Intereses")]
        public string Intereses { get; set; } = string.Empty;
    }
}
