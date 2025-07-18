namespace AppMuseo.Models
{
    public enum EstadoObra
    {
        EnMuseo,
        EnRestauracion,
        Prestada
    }

    public enum TipoColeccion
    {
        Permanente,
        Temporal
    }

    public enum TipoMovimiento
    {
        Restauracion,
        Cesion,
        Retorno
    }

    public enum TipoEntrada
    {
        General,
        Estudiante,
        Investigador,
        Discapacidad
    }
}
