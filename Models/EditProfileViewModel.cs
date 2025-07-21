using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppMuseo.Models
{
    public class EditProfileViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Display(Name = "País")]
        public string? Pais { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Foto de perfil")]
        public IFormFile? FotoPerfil { get; set; }

        public string? FotoPerfilUrl { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; } = string.Empty;

        public List<string> RolesDisponibles { get; set; } = new List<string>();

        [Display(Name = "Usuario activo")]
        public bool Activo { get; set; }

        public bool PuedeEditarRol { get; set; }

        [Display(Name = "Fecha de creación")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Último acceso")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime? UltimoAcceso { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string? CurrentPassword { get; set; }

        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y como máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string? NewPassword { get; set; }

        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y como máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string? ConfirmPassword { get; set; }

        public bool EsAdminActual { get; set; }
        public bool EsMismoUsuario { get; set; }
        public bool PuedeDesactivar { get; set; }
    }
}
