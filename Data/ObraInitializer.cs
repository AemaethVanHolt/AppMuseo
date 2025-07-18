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
                return; // Ya hay obras

            // Obtener diccionario de colecciones (nombre -> id)
            var colecciones = await ColeccionInitializer.SeedColeccionesAsync(serviceProvider);

            var obras = new List<Obra>
            {
                new Obra
                {
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1978.110.jpg",
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
                    ImageUrl = "https://www.moma.org/media/W1siZiIsIjM2NTAwIl0sWyJwIiwiY29udmVydCIsIi1yZXNpemUgMTAwMHg2NjAgXHUwMDNlIl1d.jpg?sha=7c9e8b7e6b7d9a9f",
                    Titulo = "Broadway Boogie Woogie",
                    Autor = "Piet Mondrian",
                    Anio = 1942,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "MoMA, Nueva York",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-83),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.rijksmuseum.nl/nl/collectie/SK-A-3178/tiles/0?t=1686828167378&download=true",
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
                    ImageUrl = "https://www.artic.edu/iiif/2/5b3e4c0b-5c7c-7e2b-7e1e-7e1e7e1e7e1e/full/843,/0/default.jpg",
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
                    ImageUrl = "https://www.louvre.fr/sites/default/files/medias/medias_images/images/louvre-la-liberte-guidant-le-peuple-eugene-delacroix-1824.jpg",
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
                    ImageUrl = "https://www.nationalgallery.org.uk/media/23677/ng1314-venus-and-mars-botticelli.jpg",
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
                    ImageUrl = "https://www.museoreinasofia.es/sites/default/files/obras/DE00050.jpg",
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
                    ImageUrl = "https://www.museunacional.cat/sites/default/files/styles/obra_detall/public/imatges/colleccions/ME000857.jpg",
                    Titulo = "La caza del jabalí",
                    Autor = "Pedro Berruguete",
                    Anio = 1490,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museu Nacional d'Art de Catalunya, Barcelona",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-535),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.ngv.vic.gov.au/wp-content/uploads/2015/10/NGV_2015_The-Great-Wave.jpg",
                    Titulo = "La gran ola de Kanagawa",
                    Autor = "Katsushika Hokusai",
                    Anio = 1831,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "National Gallery of Victoria, Melbourne",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-194),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.musee-orsay.fr/sites/default/files/styles/oeuvre_pleine_largeur/public/2022-11/Claude%20Monet%2C%20La%20gare%20Saint-Lazare%2C%201877.jpg?itok=8YkPqg6q",
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
                    ImageUrl = "https://www.museodelprado.es/imagenes/Documentos/imgsem/9/9f/9f1e/9f1e7e1e7e1e7e1e7e1e7e1e7e1e7e1e.jpg",
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
                    ImageUrl = "https://www.guggenheim-bilbao.eus/uploads/2019/02/peinture-130x97cm-1950.jpg",
                    Titulo = "Peinture 130 x 97 cm, 1950",
                    Autor = "Pierre Soulages",
                    Anio = 1950,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Guggenheim, Bilbao",
                    ColeccionId = colecciones["Colección temporal"],
                    FechaCreacion = DateTime.Now.AddYears(-75),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.nationalgallery.org.uk/media/23682/ng3287-the-umbrellas-renoir.jpg",
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
                    ImageUrl = "https://www.musee-orsay.fr/sites/default/files/styles/oeuvre_pleine_largeur/public/2022-11/Edouard%20Manet%2C%20Le%20dejeuner%20sur%20l%27herbe%2C%201863.jpg?itok=YkN2QKpM",
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
                    ImageUrl = "https://www.artic.edu/iiif/2/6b4e2e4e-3c7d-7e2b-7e1e-7e1e7e1e7e1e/full/843,/0/default.jpg",
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
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1977.110.jpg",
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
                    ImageUrl = "https://www.rijksmuseum.nl/nl/collectie/SK-A-3175/tiles/0?t=1686828167378&download=true",
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
                    ImageUrl = "https://www.museoreinasofia.es/sites/default/files/obras/DE00076.jpg",
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
                    ImageUrl = "https://www.museunacional.cat/sites/default/files/styles/obra_detall/public/imatges/colleccions/ME000859.jpg",
                    Titulo = "Retablo de san Miguel Arcángel",
                    Autor = "Jaume Huguet",
                    Anio = 1456,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "MNAC, Barcelona",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-569),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.museodelprado.es/imagenes/Documentos/imgsem/5/5f/5f1e/5f1e7e1e7e1e7e1e7e1e7e1e7e1e7e1e.jpg",
                    Titulo = "El sueño de Jacob",
                    Autor = "José de Ribera",
                    Anio = 1639,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo del Prado, Madrid",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-384),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1979.110.jpg",
                    Titulo = "La casa giratoria",
                    Autor = "Paul Klee",
                    Anio = 1921,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Colección temporal"],
                    FechaCreacion = DateTime.Now.AddYears(-102),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1976.110.jpg",
                    Titulo = "El violinista verde",
                    Autor = "Marc Chagall",
                    Anio = 1923,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Colección temporal"],
                    FechaCreacion = DateTime.Now.AddYears(-102),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.guggenheim-bilbao.eus/uploads/2019/02/komposition-viii-kandinsky.jpg",
                    Titulo = "Composición VIII",
                    Autor = "Wassily Kandinsky",
                    Anio = 1923,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Guggenheim, Bilbao",
                    ColeccionId = colecciones["Colección temporal"],
                    FechaCreacion = DateTime.Now.AddYears(-102),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.artic.edu/iiif/2/2e7c7e2b-7e2e-7e1e-7e1e-7e1e7e1e7e1e/full/843,/0/default.jpg",
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
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1975.110.jpg",
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
                    ImageUrl = "https://www.museodelprado.es/imagenes/Documentos/imgsem/7/7f/7f1e/7f1e7e1e7e1e7e1e7e1e7e1e7e1e7e1e.jpg",
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
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1974.110.jpg",
                    Titulo = "El sueño",
                    Autor = "Franz Marc",
                    Anio = 1912,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Colección temporal"],
                    FechaCreacion = DateTime.Now.AddYears(-113),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.rijksmuseum.nl/nl/collectie/SK-A-3176/tiles/0?t=1686828167378&download=true",
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
                    ImageUrl = "https://www.museoreinasofia.es/sites/default/files/obras/DE00077.jpg",
                    Titulo = "Figura en una ventana",
                    Autor = "Salvador Dalí",
                    Anio = 1925,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Reina Sofía, Madrid",
                    ColeccionId = colecciones["Colección permanente"],
                    FechaCreacion = DateTime.Now.AddYears(-100),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.museunacional.cat/sites/default/files/styles/obra_detall/public/imatges/colleccions/ME000860.jpg",
                    Titulo = "El descendimiento",
                    Autor = "Jaume Huguet",
                    Anio = 1465,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "MNAC, Barcelona",
                    ColeccionId = colecciones["Pintores renacentistas"],
                    FechaCreacion = DateTime.Now.AddYears(-560),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Obra
                {
                    ImageUrl = "https://www.museothyssen.org/sites/default/files/styles/obra_ficha/public/obras/1973.110.jpg",
                    Titulo = "El puerto de Hamburgo",
                    Autor = "Emil Nolde",
                    Anio = 1910,
                    Estado = EstadoObra.EnMuseo,
                    UbicacionActual = "Museo Thyssen-Bornemisza, Madrid",
                    ColeccionId = colecciones["Colección temporal"],
                    FechaCreacion = DateTime.Now.AddYears(-115),
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin@admin.com"
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
