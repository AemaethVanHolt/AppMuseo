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

                var colecciones = await ColeccionInitializer.SeedColeccionesAsync(serviceProvider);
                var fechaActual = DateTime.Now;

                var obras = new List<Obra>
                {
        // ===== PINTORES RENACENTISTAS =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/Retrato de Giovanna Tornabuoni.jpg",
                        Titulo = "Retrato de Giovanna Tornabuoni",
                        Autor = "Domenico Ghirlandaio",
                        Anio = 1489,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Renacimiento, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-535),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Venus y Marte.jpg",
                        Titulo = "Venus y Marte",
                        Autor = "Sandro Botticelli",
                        Anio = 1485,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Renacimiento, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-540),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La Gioconda.jpg",
                        Titulo = "La Gioconda",
                        Autor = "Leonardo da Vinci",
                        Anio = 1503,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Leonardo, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-522),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La escuela de Atenas.jpg",
                        Titulo = "La escuela de Atenas",
                        Autor = "Rafael Sanzio",
                        Anio = 1511,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Rafael, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-514),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El nacimiento de Venus.jpg",
                        Titulo = "El nacimiento de Venus",
                        Autor = "Sandro Botticelli",
                        Anio = 1485,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Botticelli, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-540),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La última cena.jpg",
                        Titulo = "La última cena",
                        Autor = "Leonardo da Vinci",
                        Anio = 1498,
                        Estado = EstadoObra.EnRestauracion,
                        UbicacionActual = "Taller de Restauración",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-527),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La creación de Adán.jpg",
                        Titulo = "La creación de Adán",
                        Autor = "Miguel Ángel",
                        Anio = 1512,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Miguel Ángel, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-513),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La Venus de Urbino.jpg",
                        Titulo = "La Venus de Urbino",
                        Autor = "Tiziano",
                        Anio = 1538,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Renacimiento Veneciano, Planta 1",
                        ColeccionId = colecciones["Pintores renacentistas"],
                        FechaCreacion = fechaActual.AddYears(-487),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    
        // ===== PINTORES FLAMENCOS =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/La lechera.jpg",
                        Titulo = "La lechera",
                        Autor = "Johannes Vermeer",
                        Anio = 1658,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Barroco, Planta 1",
                        ColeccionId = colecciones["Pintores flamencos"],
                        FechaCreacion = fechaActual.AddYears(-367),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La joven de la perla.jpg",
                        Titulo = "La joven de la perla",
                        Autor = "Johannes Vermeer",
                        Anio = 1665,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Barroco, Planta 1",
                        ColeccionId = colecciones["Pintores flamencos"],
                        FechaCreacion = fechaActual.AddYears(-360),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El jardín de las delicias.jpg",
                        Titulo = "El jardín de las delicias",
                        Autor = "El Bosco",
                        Anio = 1505,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Renacimiento Flamenco, Planta 1",
                        ColeccionId = colecciones["Pintores flamencos"],
                        FechaCreacion = fechaActual.AddYears(-520),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El descendimiento de la cruz.jpg",
                        Titulo = "El descendimiento de la cruz",
                        Autor = "Rogier van der Weyden",
                        Anio = 1435,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Gótico Flamenco, Planta 1",
                        ColeccionId = colecciones["Pintores flamencos"],
                        FechaCreacion = fechaActual.AddYears(-590),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El triunfo de la muerte.jpg",
                        Titulo = "El triunfo de la muerte",
                        Autor = "Pieter Brueghel el Viejo",
                        Anio = 1562,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Renacimiento Nórdico, Planta 1",
                        ColeccionId = colecciones["Pintores flamencos"],
                        FechaCreacion = fechaActual.AddYears(-463),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El matrimonio Arnolfini.png",
                        Titulo = "El matrimonio Arnolfini",
                        Autor = "Jan van Eyck",
                        Anio = 1434,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Gótico Flamenco, Planta 1",
                        ColeccionId = colecciones["Pintores flamencos"],
                        FechaCreacion = fechaActual.AddYears(-591),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    
        // ===== PINTORES SIGLO XIX =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/La Libertad guiando al pueblo.jpg",
                        Titulo = "La Libertad guiando al pueblo",
                        Autor = "Eugène Delacroix",
                        Anio = 1830,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Romanticismo, Planta 1",
                        ColeccionId = colecciones["Pintores del siglo 19"],
                        FechaCreacion = fechaActual.AddYears(-195),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La estación de Saint-Lazare.jpg",
                        Titulo = "La estación de Saint-Lazare",
                        Autor = "Claude Monet",
                        Anio = 1877,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Impresionismo, Planta 1",
                        ColeccionId = colecciones["Pintores del siglo 19"],
                        FechaCreacion = fechaActual.AddYears(-148),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Los paraguas.jpg",
                        Titulo = "Los paraguas",
                        Autor = "Pierre-Auguste Renoir",
                        Anio = 1886,
                        Estado = EstadoObra.Prestada,
                        UbicacionActual = "Museo de Orsay, París (Préstamo temporal)",
                        ColeccionId = colecciones["Pintores del siglo 19"],
                        FechaCreacion = fechaActual.AddYears(-139),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Almuerzo sobre la hierba.jpg",
                        Titulo = "Almuerzo sobre la hierba",
                        Autor = "Édouard Manet",
                        Anio = 1863,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Impresionismo, Planta 1",
                        ColeccionId = colecciones["Pintores del siglo 19"],
                        FechaCreacion = fechaActual.AddYears(-162),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/American Gothic.jpg",
                        Titulo = "American Gothic",
                        Autor = "Grant Wood",
                        Anio = 1930,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Arte Americano, Planta 2",
                        ColeccionId = colecciones["Pintores del siglo 19"],
                        FechaCreacion = fechaActual.AddYears(-95),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El bulevar de Montmartre, efecto de noche.png",
                        Titulo = "El bulevar de Montmartre, efecto de noche",
                        Autor = "Camille Pissarro",
                        Anio = 1897,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Impresionismo, Planta 1",
                        ColeccionId = colecciones["Pintores del siglo 19"],
                        FechaCreacion = fechaActual.AddYears(-128),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    
        // ===== COLECCIÓN PERMANENTE =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/El caballero de la mano en el pecho.jpg",
                        Titulo = "El caballero de la mano en el pecho",
                        Autor = "El Greco",
                        Anio = 1580,
                        Estado = EstadoObra.EnRestauracion,
                        UbicacionActual = "Taller de Restauración",
                        ColeccionId = colecciones["Colección permanente"],
                        FechaCreacion = fechaActual.AddYears(-445),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El hombre invisible.jpg",
                        Titulo = "El hombre invisible",
                        Autor = "Salvador Dalí",
                        Anio = 1929,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Surrealismo, Planta 2",
                        ColeccionId = colecciones["Colección permanente"],
                        FechaCreacion = fechaActual.AddYears(-96),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Nighthawks.jpg",
                        Titulo = "Nighthawks",
                        Autor = "Edward Hopper",
                        Anio = 1942,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Contemporánea, Planta 2",
                        ColeccionId = colecciones["Colección permanente"],
                        FechaCreacion = fechaActual.AddYears(-83),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Las dos hermanas.jpg",
                        Titulo = "Las dos hermanas",
                        Autor = "William-Adolphe Bouguereau",
                        Anio = 1881,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Academicismo, Planta 2",
                        ColeccionId = colecciones["Colección permanente"],
                        FechaCreacion = fechaActual.AddYears(-144),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El beso.jpg",
                        Titulo = "El beso",
                        Autor = "Gustav Klimt",
                        Anio = 1908,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Modernista, Planta 1",
                        ColeccionId = colecciones["Colección permanente"],
                        FechaCreacion = fechaActual.AddYears(-117),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La aparición.jpg",
                        Titulo = "La aparición",
                        Autor = "Gustave Moreau",
                        Anio = 1876,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Simbolismo, Planta 2",
                        ColeccionId = colecciones["Colección permanente"],
                        FechaCreacion = fechaActual.AddYears(-149),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
        // ===== ARTE MODERNO =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/Guernica.jpg",
                        Titulo = "Guernica",
                        Autor = "Pablo Picasso",
                        Anio = 1937,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Moderno, Planta 2",
                        ColeccionId = colecciones["Arte Moderno"],
                        FechaCreacion = fechaActual.AddYears(-86),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La Noche Estrellada.jpg",
                        Titulo = "La Noche Estrellada",
                        Autor = "Vincent van Gogh",
                        Anio = 1889,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Moderno, Planta 2",
                        ColeccionId = colecciones["Arte Moderno"],
                        FechaCreacion = fechaActual.AddYears(-134),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El Grito.jpg",
                        Titulo = "El Grito",
                        Autor = "Edvard Munch",
                        Anio = 1893,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Moderno, Planta 2",
                        ColeccionId = colecciones["Arte Moderno"],
                        FechaCreacion = fechaActual.AddYears(-130),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La Persistencia de la Memoria.png",
                        Titulo = "La Persistencia de la Memoria",
                        Autor = "Salvador Dalí",
                        Anio = 1931,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Moderno, Planta 2",
                        ColeccionId = colecciones["Arte Moderno"],
                        FechaCreacion = fechaActual.AddYears(-92),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Mujer con Sombrero.jpg",
                        Titulo = "Mujer con Sombrero",
                        Autor = "Henri Matisse",
                        Anio = 1905,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Moderno, Planta 2",
                        ColeccionId = colecciones["Arte Moderno"],
                        FechaCreacion = fechaActual.AddYears(-118),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Composición VIII.png",
                        Titulo = "Composición VIII",
                        Autor = "Wassily Kandinsky",
                        Anio = 1923,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Moderno, Planta 2",
                        ColeccionId = colecciones["Arte Moderno"],
                        FechaCreacion = fechaActual.AddYears(-100),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },

        // ===== ARTE ASIÁTICO =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/La Gran Ola de Kanagawa.jpg",
                        Titulo = "La Gran Ola de Kanagawa",
                        Autor = "Katsushika Hokusai",
                        Anio = 1831,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Asiático, Planta 1",
                        ColeccionId = colecciones["Arte Asiático"],
                        FechaCreacion = fechaActual.AddYears(-192),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El Sueño de la Esposa del Pescador.jpg",
                        Titulo = "El Sueño de la Esposa del Pescador",
                        Autor = "Katsushika Hokusai",
                        Anio = 1820,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Asiático, Planta 1",
                        ColeccionId = colecciones["Arte Asiático"],
                        FechaCreacion = fechaActual.AddYears(-203),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Pájaros y Flores de las Cuatro Estaciones.jpg",
                        Titulo = "Pájaros y Flores de las Cuatro Estaciones",
                        Autor = "Itō Jakuchū",
                        Anio = 1765,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Asiático, Planta 1",
                        ColeccionId = colecciones["Arte Asiático"],
                        FechaCreacion = fechaActual.AddYears(-258),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El monte Fuji en días claros.jpg",
                        Titulo = "El monte Fuji en días claros",
                        Autor = "Katsushika Hokusai",
                        Anio = 1830,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Asiático, Planta 1",
                        ColeccionId = colecciones["Arte Asiático"],
                        FechaCreacion = fechaActual.AddYears(-193),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El jardín de los melocotoneros en flor.jpg",
                        Titulo = "El jardín de los melocotoneros en flor",
                        Autor = "Ogata Kōrin",
                        Anio = 1705,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Asiático, Planta 1",
                        ColeccionId = colecciones["Arte Asiático"],
                        FechaCreacion = fechaActual.AddYears(-318),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El tigre bajo la lluvia.jpg",
                        Titulo = "El tigre bajo la lluvia",
                        Autor = "Maruyama Ōkyo",
                        Anio = 1780,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala de Arte Asiático, Planta 1",
                        ColeccionId = colecciones["Arte Asiático"],
                        FechaCreacion = fechaActual.AddYears(-243),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },

                    // ===== COLECCIÓN TEMPORAL =====
                    new Obra
                    {
                        ImageUrl = "/img/obras/El gran masturbador.png",
                        Titulo = "El gran masturbador",
                        Autor = "Salvador Dalí",
                        Anio = 1929,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Temporal 1, Planta -1",
                        ColeccionId = colecciones["Colección temporal"],
                        FechaCreacion = fechaActual.AddYears(-96),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/Figura en una ventana.jpg",
                        Titulo = "Figura en una ventana",
                        Autor = "Salvador Dalí",
                        Anio = 1925,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Temporal 1, Planta -1",
                        ColeccionId = colecciones["Colección temporal"],
                        FechaCreacion = fechaActual.AddYears(-96),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El hijo del hombre.png",
                        Titulo = "El hijo del hombre",
                        Autor = "René Magritte",
                        Anio = 1964,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Temporal 1, Planta -1",
                        ColeccionId = colecciones["Colección temporal"],
                        FechaCreacion = fechaActual.AddYears(-61),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/La danza.png",
                        Titulo = "La danza",
                        Autor = "Henri Matisse",
                        Anio = 1910,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Temporal 1, Planta -1",
                        ColeccionId = colecciones["Colección temporal"],
                        FechaCreacion = fechaActual.AddYears(-115),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El caminante sobre el mar de nubes.jpg",
                        Titulo = "El caminante sobre el mar de nubes",
                        Autor = "Caspar David Friedrich",
                        Anio = 1818,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Temporal 1, Planta -1",
                        ColeccionId = colecciones["Colección temporal"],
                        FechaCreacion = fechaActual.AddYears(-207),
                        UltimaModificacion = fechaActual,
                        CreadoPor = "admin"
                    },
                    new Obra
                    {
                        ImageUrl = "/img/obras/El nacimiento del mundo.jpg",
                        Titulo = "El nacimiento del mundo",
                        Autor = "Joan Miró",
                        Anio = 1925,
                        Estado = EstadoObra.EnMuseo,
                        UbicacionActual = "Sala Temporal 1, Planta -1",
                        ColeccionId = colecciones["Colección temporal"],
                        FechaCreacion = fechaActual.AddYears(-100),
                        UltimaModificacion = fechaActual,
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
