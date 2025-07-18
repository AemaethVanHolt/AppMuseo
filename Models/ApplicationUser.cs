using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AppMuseo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Pais { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<Entrada> Entradas { get; set; } = new List<Entrada>();
    }
}
