using System;

namespace AppMuseo.Models
{
    public class Obra
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public int Anio { get; set; }
        public EstadoObra Estado { get; set; }
        public string? UbicacionActual { get; set; }
        public int ColeccionId { get; set; }
        public Coleccion? Coleccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public string? CreadoPor { get; set; }
    }
}
