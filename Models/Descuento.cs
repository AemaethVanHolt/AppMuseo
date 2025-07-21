namespace AppMuseo.Models
{
    public enum TipoDescuento
    {
        Ninguno = 0,
        Estudiante = 1,
        Investigador = 2,
        Discapacidad = 3,
        TerceraEdad = 4,
        FamiliaNumerosa = 5,
        Desempleado = 6
    }

    public class Descuento
    {
        public int Id { get; set; }
        public TipoDescuento Tipo { get; set; }
        public string Descripcion => Tipo switch
        {
            TipoDescuento.Estudiante => "Estudiante",
            TipoDescuento.Investigador => "Investigador",
            TipoDescuento.Discapacidad => "Discapacidad",
            TipoDescuento.TerceraEdad => "Tercera Edad",
            TipoDescuento.FamiliaNumerosa => "Familia Numerosa",
            TipoDescuento.Desempleado => "Desempleado",
            _ => "Ninguno"
        };
        public DateTime FechaCreacion { get; set; }
    }
}
