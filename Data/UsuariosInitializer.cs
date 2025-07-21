using AppMuseo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AppMuseo.Data
{
    public static class UsuariosInitializer
    {
        public static async Task SeedUsuariosAsync(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Iniciando inicialización de usuarios...");

            try
            {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<AppMuseoDbContext>();

            // Admin (ya creado en SeedData, solo loguear)
            var adminEmail = "admin@museo.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                logger.LogError("[UsuariosInitializer] Administrador no encontrado, debe crearse en SeedData.");
                throw new Exception("El usuario administrador no existe. Asegúrate de que SeedData se haya ejecutado correctamente.");
            }

            // Restaurador
            var restEmail = "restaurador@museo.com";
            if (await userManager.FindByEmailAsync(restEmail) == null)
            {
                var rest = new ApplicationUser
                {
                    UserName = restEmail,
                    Email = restEmail,
                    EmailConfirmed = true,
                    Nombre = "Restaurador",
                    Apellidos = "Restaurador",
                    Direccion = "Calle del Arte 22, 3ºB",
                    Ciudad = "Sevilla",
                    Provincia = "Sevilla",
                    CodigoPostal = "41001",
                    Pais = "España",
                    Telefono = "688455200",
                    ImageUrl = "/img/avatares/M1.jpg",
                    FechaCreacion = DateTime.Now,
                    FechaNacimiento = new DateTime(1985, 5, 15),
                    Biografia = "Restauradora de arte con más de 10 años de experiencia en conservación de pintura al óleo.",
                    Intereses = "Restauración, Historia del Arte, Pintura Clásica",
                    Activo = true,
                    FechaRegistro = DateTime.Now
                };
                var restResult = await userManager.CreateAsync(rest, "restaurador1234");
                if (restResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(rest, "Restaurador");
                    Console.WriteLine($"[UsuariosInitializer] Restaurador creado: {restEmail}");
                }
                else
                {
                    Console.WriteLine($"[UsuariosInitializer] Error creando restaurador: {string.Join(", ", restResult.Errors.Select(e => e.Description))}");
                }
            }

            // Inicializar el generador de números aleatorios
            var random = new Random();
            
            // Datos para usuarios
            var nombres = new[] { "Luis", "Ana", "Javier", "Marta", "Elena", "Carlos", "Lucía", "Pablo", "Sara", "David", "María", "Juan", "Laura", "Diego", "Sofía", "Miguel", "Carmen", "Jorge", "Patricia", "Álvaro" };
            var apellidos = new[] { "García", "López", "Martínez", "Sánchez", "Pérez", "Gómez", "Fernández", "Ruiz", "Díaz", "Moreno", "Álvarez", "Romero", "Torres", "Navarro", "Jiménez", "Molina", "Ortega", "Delgado", "Castro", "Vargas" };
            var paises = new[] { "España", "Francia", "Italia", "Alemania", "Portugal" };
            
            // Números de teléfono de ejemplo (formato internacional)
            var telefonosDisponibles = new List<string> {
                "+34 612345678", "+34 623456789", "+34 634567890", "+34 645678901", "+34 656789012",
                "+34 667890123", "+34 678901234", "+34 690123456", "+33 612345678", "+33 623456789",
                "+33 634567890", "+39 3456789012", "+39 3456789013", "+39 3456789014", "+49 15123456789",
                "+49 15123456780", "+351 912345678", "+351 912345679", "+351 912345670", "+351 912345671"
            };
            
            var provincias = new Dictionary<string, string[]> {
                ["España"] = new[] { "Madrid", "Barcelona", "Valencia", "Sevilla", "Zaragoza", "Málaga", "Bilbao", "Alicante", "Córdoba", "Valladolid" },
                ["Francia"] = new[] { "París", "Marsella", "Lyon", "Toulouse", "Niza", "Nantes", "Estrasburgo", "Montpellier", "Burdeos", "Lille" },
                ["Italia"] = new[] { "Roma", "Milán", "Nápoles", "Turín", "Palermo", "Génova", "Bolonia", "Florencia", "Venecia", "Verona" },
                ["Alemania"] = new[] { "Berlín", "Múnich", "Hamburgo", "Colonia", "Fráncfort", "Stuttgart", "Düsseldorf", "Leipzig", "Dortmund", "Essen" },
                ["Portugal"] = new[] { "Lisboa", "Oporto", "Vila Nova de Gaia", "Amadora", "Braga", "Funchal", "Coímbra", "Setúbal", "Almada", "Agualva-Cacém" }
            };
            
            var calles = new[] { "Mayor", "Real", "Gran Vía", "San Miguel", "Plaza España", "Avenida Libertad", "Cervantes", "Goya", "Velázquez", "Picasso" };

            // Asegurarse de que los nombres, apellidos y teléfonos sean únicos
            var nombresUsados = new HashSet<string>();
            var apellidosUsados = new HashSet<string>();
            
            // Géneros para seleccionar avatares apropiados
            var generos = new[] { "male", "female" };
            var estilosAvatar = new[] { "adventurer", "avataaars", "pixel-art" };

            // Asegurarse de que el rol Visitante existe
            if (!await roleManager.RoleExistsAsync("Visitante"))
            {
                await roleManager.CreateAsync(new IdentityRole("Visitante"));
                logger.LogInformation("Rol 'Visitante' creado");
            }

            // Crear 10 visitantes con datos específicos
            var visitantes = new List<VisitanteData>
            {
                // Visitante 1
                new VisitanteData
                {
                    Nombre = "Carlos",
                    Apellidos = "Martínez Gutiérrez",
                    Email = "carlos.martinez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Calle Gran Vía 45, 3ºB",
                    Ciudad = "Madrid",
                    Provincia = "Madrid",
                    CodigoPostal = "28013",
                    Pais = "España",
                    Telefono = "609344522",
                    ImageUrl = "/img/avatares/H1.jpg",
                    FechaNacimiento = new DateTime(1985, 5, 15),
                    Biografia = "Historiador del arte especializado en el Renacimiento italiano. Me apasiona la pintura al óleo y la restauración de obras antiguas.",
                    Intereses = "Pintura clásica, Restauración, Historia del arte"
                },
                // Visitante 2
                new VisitanteData
                {
                    Nombre = "Laura",
                    Apellidos = "Sánchez López",
                    Email = "laura.sanchez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Avenida Diagonal 201, 4º2",
                    Ciudad = "Barcelona",
                    Provincia = "Barcelona",
                    CodigoPostal = "08018",
                    Pais = "España",
                    Telefono = "645277412",
                    ImageUrl = "/img/avatares/M1.jpg",
                    FechaNacimiento = new DateTime(1990, 8, 22),
                    Biografia = "Artista plástica y profesora de arte contemporáneo. Me interesa especialmente el arte conceptual y las instalaciones.",
                    Intereses = "Arte conceptual, Instalaciones, Educación artística"
                },
                // Visitante 3
                new VisitanteData
                {
                    Nombre = "David",
                    Apellidos = "García Pérez",
                    Email = "david.garcia@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Rue de Rivoli 52",
                    Ciudad = "París",
                    Provincia = "París",
                    CodigoPostal = "75004",
                    Pais = "Francia",
                    Telefono = "665902321",
                    ImageUrl = "/img/avatares/H2.jpg",
                    FechaNacimiento = new DateTime(1982, 11, 3),
                    Biografia = "Fotógrafo profesional especializado en documentar obras de arte y exposiciones para catálogos y museos.",
                    Intereses = "Fotografía, Arte moderno, Exposiciones"
                },
                // Visitante 4
                new VisitanteData
                {
                    Nombre = "Elena",
                    Apellidos = "Rodríguez Fernández",
                    Email = "elena.rodriguez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Via del Corso 120",
                    Ciudad = "Roma",
                    Provincia = "Lacio",
                    CodigoPostal = "00186",
                    Pais = "Italia",
                    Telefono = "688689480",
                    ImageUrl = "/img/avatares/M2.jpg",
                    FechaNacimiento = new DateTime(1988, 4, 17),
                    Biografia = "Guía turística especializada en museos y patrimonio histórico-artístico de Roma y el Vaticano.",
                    Intereses = "Arte renacentista, Escultura, Arquitectura"
                },
                // Visitante 5
                new VisitanteData
                {
                    Nombre = "Javier",
                    Apellidos = "López Martín",
                    Email = "javier.lopez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Kurfürstendamm 120",
                    Ciudad = "Berlín",
                    Provincia = "Berlín",
                    CodigoPostal = "10709",
                    Pais = "Alemania",
                    Telefono = "689345678",
                    ImageUrl = "/img/avatares/H3.jpg",
                    FechaNacimiento = new DateTime(1979, 9, 30),
                    Biografia = "Coleccionista de arte contemporáneo y mecenas de jóvenes artistas emergentes.",
                    Intereses = "Arte contemporáneo, Coleccionismo, Galerías"
                },
                // Visitante 6
                new VisitanteData
                {
                    Nombre = "Sofía",
                    Apellidos = "Gómez Díaz",
                    Email = "sofia.gomez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Rua Augusta 267",
                    Ciudad = "Lisboa",
                    Provincia = "Lisboa",
                    CodigoPostal = "1100-053",
                    Pais = "Portugal",
                    Telefono = "603466110",
                    ImageUrl = "/img/avatares/M3.jpg",
                    FechaNacimiento = new DateTime(1993, 12, 8),
                    Biografia = "Estudiante de Bellas Artes especializada en técnicas de pintura al fresco y muralismo.",
                    Intereses = "Pintura mural, Arte callejero, Técnicas pictóricas"
                },
                // Visitante 7
                new VisitanteData
                {
                    Nombre = "Miguel",
                    Apellidos = "Fernández Ruiz",
                    Email = "miguel.fernandez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Calle Sierpes 15, 2ºA",
                    Ciudad = "Sevilla",
                    Provincia = "Sevilla",
                    CodigoPostal = "41004",
                    Pais = "España",
                    Telefono = "658566802",
                    ImageUrl = "/img/avatares/H4.jpg",
                    FechaNacimiento = new DateTime(1980, 7, 25),
                    Biografia = "Crítico de arte y comisario de exposiciones especializado en arte barroco y su influencia en el arte actual.",
                    Intereses = "Crítica de arte, Barroco, Comisariado"
                },
                // Visitante 8
                new VisitanteData
                {
                    Nombre = "Ana",
                    Apellidos = "Pérez Sánchez",
                    Email = "ana.perez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Calle Marqués de Larios 8",
                    Ciudad = "Málaga",
                    Provincia = "Málaga",
                    CodigoPostal = "29005",
                    Pais = "España",
                    Telefono = "699621455",
                    ImageUrl = "/img/avatares/M4.jpg",
                    FechaNacimiento = new DateTime(1987, 3, 12),
                    Biografia = "Gestora cultural en el Museo Picasso Málaga. Especializada en actividades educativas y mediación cultural.",
                    Intereses = "Gestión cultural, Educación artística, Museos"
                },
                // Visitante 9
                new VisitanteData
                {
                    Nombre = "Daniel",
                    Apellidos = "González Martín",
                    Email = "daniel.gonzalez@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Calle de Serrano 45",
                    Ciudad = "Madrid",
                    Provincia = "Madrid",
                    CodigoPostal = "28001",
                    Pais = "España",
                    Telefono = "634032456",
                    ImageUrl = "/img/avatares/H5.jpg",
                    FechaNacimiento = new DateTime(1991, 10, 5),
                    Biografia = "Diseñador gráfico e ilustrador. Me interesa especialmente la relación entre el diseño y las artes plásticas.",
                    Intereses = "Diseño gráfico, Ilustración, Arte digital"
                },
                // Visitante 10
                new VisitanteData
                {
                    Nombre = "Carmen",
                    Apellidos = "Díaz Romero",
                    Email = "carmen.diaz@museum.com",
                    EmailConfirmed = true,
                    Activo = true,
                    Direccion = "Paseo de la Castellana 89",
                    Ciudad = "Madrid",
                    Provincia = "Madrid",
                    CodigoPostal = "28046",
                    Pais = "España",
                    Telefono = "677544865",
                    ImageUrl = "/img/avatares/M5.jpg",
                    FechaNacimiento = new DateTime(1984, 2, 28),
                    Biografia = "Arquitecta especializada en diseño de espacios expositivos y museográficos. Trabajo en la creación de experiencias inmersivas.",
                    Intereses = "Arquitectura, Museografía, Diseño de exposiciones"
                }
            };

            // Crear visitantes
            foreach (var (i, visitante) in visitantes.Select((v, i) => (i, v)))
            {
                var existingUser = await userManager.FindByEmailAsync(visitante.Email);
                if (existingUser != null)
                {
                    logger.LogInformation($"El usuario {visitante.Email} ya existe, omitiendo...");
                    continue;
                }

                try 
                {
                    var user = new ApplicationUser
                    {
                        UserName = visitante.Email,
                        Email = visitante.Email,
                        EmailConfirmed = true,
                        Nombre = visitante.Nombre,
                        Apellidos = visitante.Apellidos,
                        Direccion = visitante.Direccion,
                        Ciudad = visitante.Ciudad,
                        Provincia = visitante.Provincia,
                        CodigoPostal = visitante.CodigoPostal,
                        Pais = visitante.Pais,
                        PhoneNumber = visitante.Telefono, // Corregido: usar PhoneNumber en lugar de Telefono
                        ImageUrl = visitante.ImageUrl,
                        FechaCreacion = DateTime.Now.AddDays(-random.Next(1, 180)),
                        FechaNacimiento = visitante.FechaNacimiento,
                        FechaRegistro = DateTime.Now.AddDays(-random.Next(1, 365)),
                        Activo = true,
                        Biografia = visitante.Biografia,
                        Intereses = visitante.Intereses
                    };

                    var password = $"Visitante{i+1}1234!";
                    var userResult = await userManager.CreateAsync(user, password);
                    if (userResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Visitante");
                        logger.LogInformation($"[UsuariosInitializer] Visitante creado: {visitante.Email} | Contraseña: {password}");
                        
                        // Esperar un momento para asegurar que el usuario se ha guardado
                        await Task.Delay(100);
                    }
                    else
                    {
                        var errors = string.Join(", ", userResult.Errors.Select(e => e.Description));
                        logger.LogError($"[UsuariosInitializer] Error creando visitante {visitante.Email}: {errors}");
                        continue;
                    }

                    try
                    {
                        // Crear descuento
                        var descuento = new Descuento
                        {
                            Estudiante = random.Next(4) == 0, // 25% de probabilidad
                            Investigador = random.Next(5) == 0, // 20% de probabilidad
                            Discapacidad = random.Next(10) == 0, // 10% de probabilidad
                            TerceraEdad = user.FechaNacimiento <= DateTime.Now.AddYears(-65), // Mayores de 65 años
                            FamiliaNumerosa = random.Next(5) == 0, // 20% de probabilidad
                            Desempleado = random.Next(8) == 0, // 12.5% de probabilidad
                            FechaCreacion = DateTime.Now
                        };

                        // Crear extras
                        var extra = new Extra
                        {
                            AutorizacionFoto = random.Next(2) == 0, // 50% de probabilidad
                            VisitaTaller = random.Next(3) == 0, // 33% de probabilidad
                            AudioGuia = random.Next(4) == 0, // 25% de probabilidad
                            VisitaGuiada = random.Next(3) == 0, // 33% de probabilidad
                            GuiaEnLenguaExtranjera = random.Next(5) == 0, // 20% de probabilidad
                            AccesoPreferente = random.Next(10) == 0, // 10% de probabilidad
                            Parking = random.Next(4) == 0, // 25% de probabilidad
                            Tienda = random.Next(2) == 0, // 50% de probabilidad
                            FechaCreacion = DateTime.Now
                        };

                        // Guardar descuento y extra
                        await context.Descuentos.AddAsync(descuento);
                        await context.Extras.AddAsync(extra);
                        await context.SaveChangesAsync();

                        logger.LogInformation($"Descuento y extra creados para el usuario {user.Email}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error al crear descuento/extra para el usuario {user.Email}");
                        // Continuar con el siguiente usuario incluso si hay error en descuento/extra
                        continue;
                    }

                    try
                    {
                        // Obtener el último descuento y extra creados para este usuario
                        var descuento = await context.Descuentos.OrderByDescending(d => d.Id).FirstOrDefaultAsync();
                        var extra = await context.Extras.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

                        if (descuento == null || extra == null)
                        {
                            logger.LogWarning($"No se encontró descuento o extra para el usuario {user.Email}");
                            continue;
                        }

                        // Crear 1 o 2 entradas para el visitante
                        int entradasCount = 1 + random.Next(2); // 1 o 2 entradas
                        for (int j = 0; j < entradasCount; j++)
                        {
                            var tiposDisponibles = Enum.GetValues(typeof(TipoEntrada));
                            var tipoEntrada = (TipoEntrada)tiposDisponibles.GetValue(random.Next(tiposDisponibles.Length));
                            var fecha = DateTime.Now.AddDays(-random.Next(1, 30));
                            var hora = new TimeSpan(random.Next(9, 19), random.Next(0, 60), 0);
                            
                            // Seleccionar un teléfono disponible
                            string telefono;
                            if (telefonosDisponibles.Count > 0)
                            {
                                int index = random.Next(telefonosDisponibles.Count);
                                telefono = telefonosDisponibles[index];
                                telefonosDisponibles.RemoveAt(index);
                                logger.LogInformation($"Asignado teléfono {telefono} a {user.Email}");
                            }
                            else
                            {
                                // Si no hay teléfonos disponibles, generar uno aleatorio
                                telefono = $"+34 6{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";
                                logger.LogWarning($"No quedaban teléfonos disponibles, generado aleatorio: {telefono} para {user.Email}");
                            }
                            
                            // Precios base según tipo de entrada
                            decimal precioBase = tipoEntrada switch
                            {
                                TipoEntrada.Estudiante => 8.00m,
                                TipoEntrada.Investigador => 7.00m,
                                TipoEntrada.Discapacidad => 5.00m,
                                _ => 12.00m // Normal
                            };

                            // Aplicar descuentos
                            decimal total = precioBase;
                            if (descuento.Estudiante) total -= 2.00m;
                            if (descuento.Investigador) total -= 1.50m;
                            if (descuento.Discapacidad) total -= 3.00m;
                            if (descuento.TerceraEdad) total *= 0.75m; // 25% descuento
                            if (descuento.FamiliaNumerosa) total *= 0.80m; // 20% descuento
                            if (descuento.Desempleado) total *= 0.50m; // 50% descuento
                            
                            // Precio mínimo
                            if (total < 1.00m) total = 1.00m;



                            // Crear entrada
                            var entrada = new Entrada
                            {
                                Fecha = fecha.Date,
                                Hora = hora,
                                Precio = precioBase,
                                TipoEntrada = tipoEntrada,
                                IncluyeAutorizacionFoto = extra.AutorizacionFoto,
                                IncluyeVisitaTaller = extra.VisitaTaller,
                                IncluyeAudioGuia = extra.AudioGuia,
                                IncluyeVisitaGuiada = extra.VisitaGuiada,
                                IncluyeGuiaExtranjera = extra.GuiaEnLenguaExtranjera,
                                IncluyeAccesoPreferente = extra.AccesoPreferente,
                                IncluyeParking = extra.Parking,
                                IncluyeTienda = extra.Tienda,
                                Total = Math.Round(total, 2), // Redondear a 2 decimales
                                UserId = user.Id,
                                DescuentoId = descuento.Id,
                                ExtraId = extra.Id,
                                FechaCreacion = DateTime.Now,
                                UltimaModificacion = DateTime.Now,
                                CreadoPor = adminEmail
                            };

                            await context.Entradas.AddAsync(entrada);
                            await context.SaveChangesAsync();
                            
                            logger.LogInformation($"Entrada {j+1} creada para {user.Email} - Tipo: {tipoEntrada}, Precio: {entrada.Total:C2}");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error al crear entradas para el usuario {user.Email}");
                        // Continuar con el siguiente usuario incluso si hay error en entradas
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error al procesar el visitante {visitante.Email}");
                    // Continuar con el siguiente visitante si hay un error
                    continue;
                }
            }
            
            logger.LogInformation("\n" + new string('=', 100));
            logger.LogInformation("DETALLES COMPLETOS DE LOS USUARIOS");
            logger.LogInformation(new string('=', 100));
            
            // Mostrar credenciales de acceso
            logger.LogInformation("\n" + new string('-', 50));
            logger.LogInformation("CREDENCIALES DE ACCESO");
            logger.LogInformation(new string('-', 50));
            logger.LogInformation($"Admin: admin@museo.com | Contraseña: admin1234");
            logger.LogInformation($"Restaurador: restaurador@museo.com | Contraseña: restaurador1234");
            
            // Obtener solo los 10 primeros visitantes
            var usuarios = await userManager.Users
                .Where(u => u.Email != adminEmail && u.Email != "restaurador@museo.com")
                .Take(10)
                .ToListAsync();
            
            logger.LogInformation("\n" + new string('=', 100));
            logger.LogInformation("DATOS DE LOS VISITANTES");
            logger.LogInformation(new string('=', 100));
            
            int contador = 1;
            foreach (var user in usuarios)
            {
                var roles = await userManager.GetRolesAsync(user);
                
                logger.LogInformation($"\n// Visitante {contador++} - {user.Email}");
                logger.LogInformation($"new ApplicationUser");
                logger.LogInformation($"{{");
                logger.LogInformation($"    UserName = \"{user.UserName}\",");
                logger.LogInformation($"    Email = \"{user.Email}\",");
                logger.LogInformation($"    EmailConfirmed = true,");
                logger.LogInformation($"    Nombre = \"{user.Nombre}\",");
                logger.LogInformation($"    Apellidos = \"{user.Apellidos}\",");
                logger.LogInformation($"    Direccion = \"{user.Direccion}\",");
                logger.LogInformation($"    Ciudad = \"{user.Ciudad}\",");
                logger.LogInformation($"    Provincia = \"{user.Provincia}\",");
                logger.LogInformation($"    CodigoPostal = \"{user.CodigoPostal}\",");
                logger.LogInformation($"    Pais = \"{user.Pais}\",");
                logger.LogInformation($"    Telefono = \"{user.PhoneNumber}\",");
                logger.LogInformation($"    ImageUrl = \"{user.ImageUrl}\",");
                logger.LogInformation($"    FechaNacimiento = new DateTime({user.FechaNacimiento:yyyy, MM, dd}),");
                logger.LogInformation($"    FechaRegistro = DateTime.Now,");
                logger.LogInformation($"    Activo = {user.Activo.ToString().ToLower()},");
                logger.LogInformation($"    Biografia = @\"{user.Biografia}\",");
                logger.LogInformation($"    Intereses = \"{user.Intereses}\"");
                logger.LogInformation($"}};");
                
                if (roles.Any())
                {
                    logger.LogInformation($"// Roles: {string.Join(", ", roles)}");
                }
                logger.LogInformation(new string('-', 100));
            }
            
            logger.LogInformation("\n" + new string('=', 100));
            logger.LogInformation("FIN DEL LISTADO DE USUARIOS");
            logger.LogInformation(new string('=', 100));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al inicializar usuarios: {Message}", ex.Message);
                throw new Exception("Error al inicializar usuarios. Ver el log para más detalles.", ex);
            }
        }
    }
}
