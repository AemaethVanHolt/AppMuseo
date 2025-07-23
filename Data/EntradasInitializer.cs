using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AppMuseo.Data
{
    public class EntradasInitializer
    {
        public static async Task SeedEntradasAsync(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<AppMuseoDbContext>();
            var random = new Random();

            // Obtener el email del administrador
            var adminEmail = "admin@museo.com";

            // Precios base
            var preciosBase = new
            {
                Normal = 17.00m,
                Estudiante = 8.00m,
                Investigador = 7.00m,
                Discapacidad = 0.00m
            };

            // Tipos de descuento disponibles
            var tiposDescuento = new[]
            {
                TipoDescuento.Estudiante,
                TipoDescuento.Investigador,
                TipoDescuento.Discapacidad,
                TipoDescuento.TerceraEdad,
                TipoDescuento.FamiliaNumerosa,
                TipoDescuento.Desempleado
            };
            
            // Porcentajes de descuento
            var porcentajesDescuento = new Dictionary<TipoDescuento, decimal>
            {
                { TipoDescuento.Estudiante, 2.00m },
                { TipoDescuento.Investigador, 1.50m },
                { TipoDescuento.Discapacidad, 0.00m },
                { TipoDescuento.TerceraEdad, 0.20m }, // 20%
                { TipoDescuento.FamiliaNumerosa, 0.25m }, // 25%
                { TipoDescuento.Desempleado, 0.50m } // 50%
            };

            // Extras
            var extras = new
            {
                AutorizacionFoto = 6.00m,
                VisitaTaller = 15.00m,
                AudioGuia = 2.50m,
                VisitaGuiada = 5.00m,
                GuiaExtranjera = 3.00m,
                AccesoPreferente = 4.00m,
                Parking = 12.00m,
                Tienda = 0.00m
            };

            // Mostrar precios y descuentos
            logger.LogInformation("\n" + new string('=', 50));
            logger.LogInformation("PRECIOS Y DESCUENTOS");
            logger.LogInformation(new string('=', 50));
            logger.LogInformation("\nPRECIOS BASE:");
            logger.LogInformation($"- Normal: {preciosBase.Normal:C2}");
            logger.LogInformation($"- Estudiante: {preciosBase.Estudiante:C2}");
            logger.LogInformation($"- Investigador: {preciosBase.Investigador:C2}");
            logger.LogInformation($"- Discapacidad: {preciosBase.Discapacidad:C2}");

            logger.LogInformation("\nDESCUENTOS:");
            logger.LogInformation($"- Estudiante: -2.00 € (fijo)");
            logger.LogInformation($"- Investigador: -1.50 € (fijo)");
            logger.LogInformation($"- Discapacidad: Gratis");
            logger.LogInformation("- Tercera Edad: -25% del precio base");
            logger.LogInformation("- Familia Numerosa: -20% del precio base");
            logger.LogInformation("- Desempleado: -50% del precio base");

            logger.LogInformation("\nEXTRAS:");
            logger.LogInformation($"- Autorización Foto: {extras.AutorizacionFoto:C2}");
            logger.LogInformation($"- Visita Taller: {extras.VisitaTaller:C2}");
            logger.LogInformation($"- Audioguía: {extras.AudioGuia:C2}");
            logger.LogInformation($"- Visita Guiada: {extras.VisitaGuiada:C2}");
            logger.LogInformation($"- Guía en Lengua Extranjera: {extras.GuiaExtranjera:C2} (adicional a visita guiada)");
            logger.LogInformation($"- Acceso Preferente: {extras.AccesoPreferente:C2}");
            logger.LogInformation($"- Parking: {extras.Parking:C2}");
            logger.LogInformation($"- Tienda: {extras.Tienda:C2} (solo seguimiento)");

            // Obtener todos los usuarios excepto admin y restaurador
            var usuarios = await userManager.Users
                .Where(u => u.Email != adminEmail && u.Email != "restaurador@museo.com")
                .ToListAsync();

            logger.LogInformation("\n" + new string('=', 50));
            logger.LogInformation("GENERANDO ENTRADAS");
            logger.LogInformation(new string('=', 50));

            foreach (var usuario in usuarios)
            {
                // Determinar cuántas entradas tendrá el usuario (1-3)
                int numEntradas = random.Next(1, 4);
                logger.LogInformation($"\nGenerando {numEntradas} entradas para {usuario.Nombre} {usuario.Apellidos}:");

                for (int i = 0; i < numEntradas; i++)
                {
                    // Seleccionar tipo de entrada aleatorio
                    var tiposEntrada = Enum.GetValues(typeof(TipoEntrada));
                    if (tiposEntrada == null || tiposEntrada.Length == 0)
                    {
                        logger.LogWarning("No se encontraron tipos de entrada disponibles, usando valor predeterminado");
                        continue;
                    }
                    var tipoEntrada = (TipoEntrada)(tiposEntrada.GetValue(random.Next(tiposEntrada.Length)) ?? TipoEntrada.Normal);

                    // Calcular precio base según tipo de entrada
                    decimal precioBase = tipoEntrada switch
                    {
                        TipoEntrada.Normal => preciosBase.Normal,
                        TipoEntrada.Estudiante => preciosBase.Estudiante,
                        TipoEntrada.Investigador => preciosBase.Investigador,
                        TipoEntrada.Discapacidad => preciosBase.Discapacidad,
                        _ => preciosBase.Normal
                    };

                    // Determinar si se aplica algún descuento (70% de probabilidad)
                    bool aplicarDescuento = random.Next(10) < 7; // 70% de probabilidad
                    Descuento descuento = null;
                    decimal total = precioBase;
                    
                    if (aplicarDescuento)
                    {
                        // Seleccionar un tipo de descuento aleatorio
                        var tipoDescuento = tiposDescuento[random.Next(tiposDescuento.Length)];
                        
                        // Crear el descuento
                        descuento = new Descuento
                        {
                            Tipo = tipoDescuento,
                            FechaCreacion = DateTime.Now
                        };
                        
                        // Aplicar el descuento correspondiente
                        if (tipoDescuento == TipoDescuento.Discapacidad)
                        {
                            total = 0; // Gratis para discapacitados
                        }
                        else if (tipoDescuento == TipoDescuento.TerceraEdad || 
                                tipoDescuento == TipoDescuento.FamiliaNumerosa || 
                                tipoDescuento == TipoDescuento.Desempleado)
                        {
                            // Aplicar descuento porcentual
                            decimal porcentaje = porcentajesDescuento[tipoDescuento];
                            total -= precioBase * porcentaje;
                        }
                        else
                        {
                            // Descuento fijo para estudiante o investigador
                            total -= porcentajesDescuento[tipoDescuento];
                        }
                    }

                    // Asegurar que el precio no sea negativo
                    total = Math.Max(0, total);

                    // Crear extras
                    var extra = new Extra
                    {
                        AutorizacionFoto = random.Next(2) == 1, // 50%
                        VisitaTaller = random.Next(3) == 0, // 33%
                        AudioGuia = random.Next(4) == 0, // 25%
                        VisitaGuiada = random.Next(3) == 0, // 33%
                        GuiaEnLenguaExtranjera = random.Next(5) == 0, // 20%
                        AccesoPreferente = random.Next(10) == 0, // 10%
                        Parking = random.Next(4) == 0, // 25%
                        Tienda = random.Next(2) == 0, // 50%
                        FechaCreacion = DateTime.Now
                    };

                    // Calcular total con extras
                    if (extra.AutorizacionFoto) total += extras.AutorizacionFoto;
                    if (extra.VisitaTaller) total += extras.VisitaTaller;
                    if (extra.AudioGuia) total += extras.AudioGuia;
                    if (extra.VisitaGuiada)
                    {
                        total += extras.VisitaGuiada;
                        if (extra.GuiaEnLenguaExtranjera) total += extras.GuiaExtranjera;
                    }
                    if (extra.AccesoPreferente) total += extras.AccesoPreferente;
                    if (extra.Parking) total += extras.Parking;

                    // Crear fecha y hora de la entrada (en los últimos 30 días, horario de 9:00 a 20:00)
                    var fechaEntrada = DateTime.Now.AddDays(-random.Next(30));
                    var horaEntrada = new TimeSpan(9 + random.Next(10), random.Next(2) * 30, 0); // 9:00-19:00 en intervalos de 30 min

                    // Crear la entrada
                    var entrada = new Entrada
                    {
                        Fecha = fechaEntrada.Date,
                        Hora = horaEntrada,
                        Precio = precioBase,
                        Total = total,
                        TipoEntrada = tipoEntrada,
                        IncluyeAutorizacionFoto = extra.AutorizacionFoto,
                        IncluyeVisitaTaller = extra.VisitaTaller,
                        IncluyeAudioGuia = extra.AudioGuia,
                        IncluyeVisitaGuiada = extra.VisitaGuiada,
                        IncluyeGuiaExtranjera = extra.GuiaEnLenguaExtranjera,
                        IncluyeAccesoPreferente = extra.AccesoPreferente,
                        IncluyeParking = extra.Parking,
                        IncluyeTienda = extra.Tienda,
                        UserId = usuario.Id,
                        Descuento = descuento,
                        Extra = extra,
                        FechaCreacion = DateTime.Now,
                        UltimaModificacion = DateTime.Now,
                        CreadoPor = adminEmail
                    };

                    // Guardar en la base de datos si hay descuento
                    if (descuento != null)
                    {
                        context.Descuentos.Add(descuento);
                        // Guardar para obtener el ID generado
                        await context.SaveChangesAsync();
                    }
                    
                    context.Extras.Add(extra);
                    context.Entradas.Add(entrada);
                    await context.SaveChangesAsync();

                    // Mostrar resumen de la entrada
                    logger.LogInformation($"\nEntrada {i + 1}:");
                    logger.LogInformation($"- Fecha: {entrada.Fecha:dd/MM/yyyy} {entrada.Hora.Hours:00}:{entrada.Hora.Minutes:00}");
                    logger.LogInformation($"- Tipo: {entrada.TipoEntrada}");
                    logger.LogInformation($"- Precio base: {entrada.Precio:C2}");
                    
                    // Mostrar descuento aplicado si existe
                    if (descuento != null && descuento.Tipo != TipoDescuento.Ninguno)
                    {
                        logger.LogInformation($"- Descuento: {descuento.Descripcion}");
                    }
                    else
                    {
                        logger.LogInformation("- Sin descuentos");
                    }
                    
                    // Mostrar extras
                    var extrasAplicados = new System.Text.StringBuilder("- Extras: ");
                    bool tieneExtras = false;
                    
                    if (extra.AutorizacionFoto) { extrasAplicados.Append("Foto, "); tieneExtras = true; }
                    if (extra.VisitaTaller) { extrasAplicados.Append("Taller, "); tieneExtras = true; }
                    if (extra.AudioGuia) { extrasAplicados.Append("Audioguía, "); tieneExtras = true; }
                    if (extra.VisitaGuiada)
                    {
                        extrasAplicados.Append("Visita Guiada");
                        if (extra.GuiaEnLenguaExtranjera) extrasAplicados.Append(" (Extranjera)");
                        extrasAplicados.Append(", ");
                        tieneExtras = true;
                    }
                    if (extra.AccesoPreferente) { extrasAplicados.Append("Acceso Preferente, "); tieneExtras = true; }
                    if (extra.Parking) { extrasAplicados.Append("Parking, "); tieneExtras = true; }
                    if (extra.Tienda) { extrasAplicados.Append("Tienda, "); tieneExtras = true; }
                    
                    if (tieneExtras)
                        logger.LogInformation(extrasAplicados.ToString().TrimEnd(',', ' '));
                    else
                        logger.LogInformation("- Sin extras");
                    
                    logger.LogInformation($"- Total a pagar: {entrada.Total:C2}");
                }
            }

            await context.SaveChangesAsync();
            logger.LogInformation("\nEntradas generadas exitosamente.");
        }
    }
}
