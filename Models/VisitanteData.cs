using System;

namespace AppMuseo.Models
{
    public class VisitanteData
    {
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Activo { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Pais { get; set; }
        public string? Telefono { get; set; }
        public string? ImageUrl { get; set; }
        public string? FotoPerfil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Biografia { get; set; }
        public string? Intereses { get; set; }
    }
}
