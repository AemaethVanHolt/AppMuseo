using System.ComponentModel.DataAnnotations;

namespace AppMuseo.Models.ViewModels
{
    public class ReservarEntradaViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;
        
        [Display(Name = "Artista")]
        public string Artista { get; set; } = string.Empty;
        
        [Display(Name = "Fechas")]
        public string Fecha { get; set; } = string.Empty;
        
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;
        
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; } = string.Empty;
        
        [Display(Name = "Imagen")]
        public string Imagen { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, 10, ErrorMessage = "La cantidad debe estar entre 1 y 10")]
        public int Cantidad { get; set; } = 1;
        
        [Display(Name = "Fecha de visita")]
        [Required(ErrorMessage = "La fecha de visita es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaVisita { get; set; } = DateTime.Today;
        
        [Display(Name = "Hora de visita")]
        [Required(ErrorMessage = "La hora de visita es obligatoria")]
        [DataType(DataType.Time)]
        public TimeSpan HoraVisita { get; set; } = new TimeSpan(10, 0, 0);
        
        [Display(Name = "Incluir audioguía")]
        public bool IncluirAudioGuia { get; set; }
        
        [Display(Name = "Incluir visita guiada")]
        public bool IncluirVisitaGuiada { get; set; }
        
        [Display(Name = "Incluir acceso a exposición temporal")]
        public bool IncluirExposicionTemporal { get; set; }
        
        [Display(Name = "Precio total")]
        [DataType(DataType.Currency)]
        public decimal PrecioTotal { get; set; }
        
        // Precios base
        public decimal PrecioBase { get; set; } = 12.00m;
        public decimal PrecioAudioGuia { get; set; } = 3.50m;
        public decimal PrecioVisitaGuiada { get; set; } = 5.00m;
        public decimal PrecioExposicionTemporal { get; set; } = 4.50m;
    }
}
