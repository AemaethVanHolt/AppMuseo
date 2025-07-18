using System.Collections.Generic;
using System;

namespace AppMuseo.Models
{
    public class Coleccion
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public TipoColeccion Tipo { get; set; }
        public string? Descripcion { get; set; }
        public ICollection<Obra> Obras { get; set; } = new List<Obra>();
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public string? CreadoPor { get; set; }
    }
}
