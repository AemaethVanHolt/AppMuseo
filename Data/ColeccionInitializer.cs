using AppMuseo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppMuseo.Data
{
    public static class ColeccionInitializer
    {
        public static async Task<Dictionary<string, int>> SeedColeccionesAsync(IServiceProvider serviceProvider)
        {
            try
            {
            var context = serviceProvider.GetRequiredService<AppMuseoDbContext>();
            var colecciones = new List<Coleccion>
            {
                new Coleccion
                {
                    Nombre = "Pintores flamencos",
                    Tipo = TipoColeccion.Permanente,
                    Descripcion = "Obras de pintores flamencos de los siglos XV al XVII.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Coleccion
                {
                    Nombre = "Pintores del siglo 19",
                    Tipo = TipoColeccion.Permanente,
                    Descripcion = "Obras de pintores destacados del siglo XIX.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Coleccion
                {
                    Nombre = "Pintores renacentistas",
                    Tipo = TipoColeccion.Permanente,
                    Descripcion = "Obras maestras del Renacimiento europeo.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Coleccion
                {
                    Nombre = "Colección temporal",
                    Tipo = TipoColeccion.Temporal,
                    Descripcion = "Exposición temporal de obras internacionales.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Coleccion
                {
                    Nombre = "Colección permanente",
                    Tipo = TipoColeccion.Permanente,
                    Descripcion = "Selección permanente de obras del museo.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Coleccion
                {
                    Nombre = "Arte Moderno",
                    Tipo = TipoColeccion.Permanente,
                    Descripcion = "Obras de arte moderno de los siglos XX y XXI.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                },
                new Coleccion
                {
                    Nombre = "Arte Asiático",
                    Tipo = TipoColeccion.Permanente,
                    Descripcion = "Arte tradicional y contemporáneo de Asia, incluyendo pinturas, esculturas y grabados.",
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    CreadoPor = "admin"
                }
            };

            // Insertar solo si no existen
            foreach (var col in colecciones)
            {
                if (!await context.Colecciones.AnyAsync(c => c.Nombre == col.Nombre))
                {
                    context.Colecciones.Add(col);
                }
            }
            await context.SaveChangesAsync();

            // Devolver dic Nombre -> Id
            var dic = new Dictionary<string, int>();
            foreach (var col in colecciones)
            {
                try
                {
                    if (col.Nombre != null)
                    {
                        var dbCol = await context.Colecciones.FirstOrDefaultAsync(c => c.Nombre == col.Nombre);
                        if (dbCol != null)
                        {
                            dic[col.Nombre] = dbCol.Id;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ColeccionInitializer] Error al obtener colección {col.Nombre}: {ex.Message}\n{ex.StackTrace}");
                }
            }
            Console.WriteLine($"[ColeccionInitializer] Colecciones insertadas: {colecciones.Count}");
            return dic;            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ColeccionInitializer] Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
