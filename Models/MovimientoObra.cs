using System;

namespace AppMuseo.Models
{
    public class MovimientoObra
    {
        public int Id { get; set; }
        public int ObraId { get; set; }
        public Obra? Obra { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public string? UbicacionDestino { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public string? CreadoPor { get; set; }
    }
}
