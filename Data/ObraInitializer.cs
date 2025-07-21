using AppMuseo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMuseo.Data
{
    public static class ObraInitializer
    {
        public static async Task SeedObrasAsync(IServiceProvider serviceProvider)
        {
            try
            {
            var context = serviceProvider.GetRequiredService<AppMuseoDbContext>();
            if (await context.Obras.AnyAsync())
                return; 

            // Obtener diccionario de colecciones (nombre -> id)
            var colecciones = await ColeccionInitializer.SeedColeccionesAsync(serviceProvider);

            var obras = new List<Obra>
            {
                new Obra
                {
                    ImageUrl = "/img/obras/Retrato de Giovanna Tornabuoni.jpg",
                    Titulo = "Retrato de Giovanna Tornabuoni",
                    Autor = "Domenico Ghirlandaio",
                    Anio = 1489,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-535),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/La lechera.jpg",
                    Titulo = "La lechera",
                    Autor = "Johannes Vermeer",
                    Anio = 1658,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Rijksmuseum, Ámsterdam",
                    ColeccionId = colecciones["Pintores flamencos"],
                    FechaCreacion = DateTime.Now.AddYears(-367),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/Nighthawks.jpg",
                    Titulo = "Nighthawks",
                    Autor = "Edward Hopper",
                    Anio = 1942,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Art Institute of Chicago",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-83),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/La Libertad guiando al pueblo.jpg",
                    Titulo = "La Libertad guiando al pueblo",
                    Autor = "Eugène Delacroix",
                    Anio = 1830,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo del Louvre, París",
                    ColeccionId = colecciones["Pintores del siglo 19"],
                    FechaCreacion = DateTime.Now.AddYears(-195),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/Venus y Marte.jpg",
                    Titulo = "Venus y Marte",
                    Autor = "Sandro Botticelli",
                    Anio = 1485,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "National Gallery, Londres",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-540),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/El hombre invisible.jpg",
                    Titulo = "El hombre invisible",
                    Autor = "Salvador Dalí",
                    Anio = 1929,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Reina Sofía, Madrid",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-96),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/La estación de Saint-Lazare.jpg",
                    Titulo = "La estación de Saint-Lazare",
                    Autor = "Claude Monet",
                    Anio = 1877,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo de Orsay, París",
                    ColeccionId = colecciones["Pintores del siglo 19"],
                    FechaCreacion = DateTime.Now.AddYears(-148),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/El caballero de la mano en el pecho.jpg",
                    Titulo = "El caballero de la mano en el pecho",
                    Autor = "El Greco",
                    Anio = 1580,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo del Prado, Madrid",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-445),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/Los paraguas.jpg",
                    Titulo = "Los paraguas",
                    Autor = "Pierre-Auguste Renoir",
                    Anio = 1886,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "National Gallery, Londres",
                    ColeccionId = colecciones["Pintores del siglo 19"],
                    FechaCreacion = DateTime.Now.AddYears(-139),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/Almuerzo sobre la hierba.jpg",
                    Titulo = "Almuerzo sobre la hierba",
                    Autor = "Édouard Manet",
                    Anio = 1863,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo de Orsay, París",
                    ColeccionId = colecciones["Pintores del siglo 19"],
                    FechaCreacion = DateTime.Now.AddYears(-162),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/American Gothic.jpg",
                    Titulo = "American Gothic",
                    Autor = "Grant Wood",
                    Anio = 1930,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Art Institute of Chicago",
                    ColeccionId = colecciones["Pintores del siglo 19"],
                    FechaCreacion = DateTime.Now.AddYears(-95),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/Vista de Delft.jpg",
                    Titulo = "Vista de Delft",
                    Autor = "Carel Fabritius",
                    Anio = 1652,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Pintores flamencos"],
                    FechaCreacion = DateTime.Now.AddYears(-371),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/El alegre bebedor.jpg",
                    Titulo = "El alegre bebedor",
                    Autor = "Frans Hals",
                    Anio = 1628,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Rijksmuseum, Ámsterdam",
                    ColeccionId = colecciones["Pintores flamencos"],
                    FechaCreacion = DateTime.Now.AddYears(-396),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/El gran masturbador.png",
                    Titulo = "El gran masturbador",
                    Autor = "Salvador Dalí",
                    Anio = 1929,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Reina Sofía, Madrid",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-96),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/La habitación de hotel.jpg",
                    Titulo = "La habitación de hotel",
                    Autor = "Edward Hopper",
                    Anio = 1931,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Art Institute of Chicago",
                    ColeccionId = colecciones["Pintores del siglo 19"],
                    FechaCreacion = DateTime.Now.AddYears(-94),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/La plaza de San Marcos en Venecia.jpg",
                    Titulo = "La plaza de San Marcos en Venecia",
                    Autor = "Canaletto",
                    Anio = 1723,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-302),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/El jardín de las delicias.jpg",
                    Titulo = "El jardín de las delicias (detalle)",
                    Autor = "El Bosco",
                    Anio = 1500,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo del Prado, Madrid",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-525),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/La companía de Frans Banning Cocq.jpg",
                    Titulo = "La compañía de Frans Banning Cocq",
                    Autor = "Govert Flinck",
                    Anio = 1642,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Rijksmuseum, Ámsterdam",
                    ColeccionId = colecciones["Pintores flamencos"],
                    FechaCreacion = DateTime.Now.AddYears(-381),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "/img/obras/Figura en una ventana.jpg",
                    Titulo = "Figura en una ventana",
                    Autor = "Salvador Dalí",
                    Anio = 1925,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Reina Sofía, Madrid",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-100),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                }
            };

                await context.Obras.AddRangeAsync(obras);
                await context.SaveChangesAsync();
                Console.WriteLine($"[ObraInitializer] Obras insertadas: {obras.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ObraInitializer] Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
