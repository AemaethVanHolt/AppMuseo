namespace AppMuseo.Models
{
    public class Descuento
    {
        public int Id { get; set; }
        public bool Estudiante { get; set; }
        public bool Investigador { get; set; }
        public bool Discapacidad { get; set; }
        public bool TerceraEdad { get; set; }
        public bool FamiliaNumerosa { get; set; }
        public bool Desempleado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
