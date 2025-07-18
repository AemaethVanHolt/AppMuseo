using System;

namespace AppMuseo.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int EntradaId { get; set; }
        public Entrada? Entrada { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public bool Confirmado { get; set; }
        public string? PdfUrl { get; set; }
        public string? QrUrl { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public string? CreadoPor { get; set; }
    }
}
