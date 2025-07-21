namespace AppMuseo.Models
{
    public class Extra
    {
        public int Id { get; set; }
        public bool AutorizacionFoto { get; set; }
        public bool VisitaTaller { get; set; }
        public bool AudioGuia { get; set; }
        public bool VisitaGuiada { get; set; }
        public bool GuiaEnLenguaExtranjera { get; set; }
        public bool AccesoPreferente { get; set; }
        public bool Parking { get; set; }
        public bool Tienda { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
