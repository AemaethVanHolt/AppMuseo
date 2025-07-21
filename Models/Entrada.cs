using System;
using System.Collections.Generic;

namespace AppMuseo.Models
{
    public class Entrada
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Precio { get; set; }
        public TipoEntrada TipoEntrada { get; set; }
        public bool IncluyeAutorizacionFoto { get; set; }
        public bool IncluyeVisitaTaller { get; set; }
        public bool IncluyeAudioGuia { get; set; }
        public bool IncluyeVisitaGuiada { get; set; }
        public bool IncluyeGuiaExtranjera { get; set; }
        public bool IncluyeAccesoPreferente { get; set; }
        public bool IncluyeParking { get; set; }
        public bool IncluyeTienda { get; set; }
        public decimal Total { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        
        // Relación con Descuento
        public int? DescuentoId { get; set; }
        public Descuento? Descuento { get; set; }
        
        // Relación con Extra
        public int? ExtraId { get; set; }
        public Extra? Extra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public string? CreadoPor { get; set; }
    }
}
