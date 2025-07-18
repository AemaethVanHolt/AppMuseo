using AppMuseo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
            try
            {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<AppMuseoDbContext>();

            // Admin (ya creado en SeedData, solo loguear)
            var adminEmail = "admin@admin.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                Console.WriteLine("[UsuariosInitializer] Admin no encontrado, debe crearse en SeedData.");
                return;
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
                    Nombre = "Luna",
                    Apellidos = "Restauradora",
                    Direccion = "Calle del Arte 22, Sevilla",
                    Pais = "España",
                    Telefono = "+34 678901234",
                    ImageUrl = "https://avatars.dicebear.com/api/avataaars/Luna_Restauradora.svg",
                    FechaCreacion = DateTime.Now,
                    FechaNacimiento = DateTime.Now.AddYears(-35)
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

            // Visitantes
            var nombres = new[] { "Luis", "Ana", "Javier", "Marta", "Elena", "Carlos", "Lucía", "Pablo", "Sara", "David" };
            var apellidos = new[] { "García", "López", "Martínez", "Sánchez", "Pérez", "Gómez", "Fernández", "Ruiz", "Díaz", "Moreno" };
            var paises = new[] { "España", "Francia", "Italia", "Alemania", "Portugal", "Argentina", "México", "Chile", "Colombia", "Estados Unidos" };
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var email = $"visitante{i+1}@museum.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        Nombre = nombres[i],
                        Apellidos = apellidos[i],
                        Direccion = $"Calle Falsa {random.Next(1, 100)}, Ciudad {random.Next(1, 10)}",
                        Pais = paises[random.Next(paises.Length)],
                        Telefono = $"+34 6{random.Next(10000000, 99999999)}",
                        ImageUrl = $"https://avatars.dicebear.com/api/avataaars/{nombres[i]}_{apellidos[i]}.svg",
                        FechaCreacion = DateTime.Now.AddDays(-random.Next(1, 180)),
                        FechaNacimiento = DateTime.Now.AddYears(-random.Next(0, 100))
                    };
                    var userResult = await userManager.CreateAsync(user, $"visitante{i+1}1234");
                    if (userResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Visitante");
                        Console.WriteLine($"[UsuariosInitializer] Visitante creado: {email}");
                    }
                    else
                    {
                        Console.WriteLine($"[UsuariosInitializer] Error creando visitante {email}: {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
                        continue;
                    }

                    // Extras y descuentos realistas
                    var descuento = new Descuento
                    {
                        Estudiante = random.Next(2) == 0,
                        Investigador = random.Next(3) == 0,
                        Discapacidad = random.Next(4) == 0
                    };
                    var extra = new Extra
                    {
                        AutorizacionFoto = random.Next(2) == 0,
                        VisitaTaller = random.Next(3) == 0,
                        AudioGuia = random.Next(2) == 0,
                        VisitaGuiada = random.Next(3) == 0
                    };
                    context.Descuentos.Add(descuento);
                    context.Extras.Add(extra);
                    await context.SaveChangesAsync();

                    // Crear 1 o 2 entradas para el visitante
                    int entradasCount = 1 + random.Next(2); // 1 o 2 entradas
                    for (int j = 0; j < entradasCount; j++)
                    {
                        var tipoEntrada = (TipoEntrada)random.Next(Enum.GetValues(typeof(TipoEntrada)).Length);
                        var fecha = DateTime.Now.AddDays(-random.Next(1, 30));
                        var hora = new TimeSpan(random.Next(9, 19), random.Next(0, 60), 0);
                        decimal precioBase = tipoEntrada == TipoEntrada.Estudiante ? 8 : tipoEntrada == TipoEntrada.Investigador ? 7 : tipoEntrada == TipoEntrada.Discapacidad ? 5 : 12;
                        decimal total = precioBase;
                        if (descuento.Estudiante) total -= 2;
                        if (descuento.Investigador) total -= 1.5m;
                        if (descuento.Discapacidad) total -= 3;
                        if (total < 2) total = 2;
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
                            Total = total,
                            UserId = user.Id,
                            User = user,
                            Descuento = descuento,
                            Extra = extra,
                            FechaCreacion = fecha,
                            UltimaModificacion = DateTime.Now,
                            CreadoPor = adminEmail
                        };
                        context.Entradas.Add(entrada);
                        await context.SaveChangesAsync();
                    }
                }
            }
            // Mostrar por consola los emails y contraseñas de los visitantes
            Console.WriteLine("Usuarios visitantes generados:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Email: visitante{i+1}@museum.com | Password: visitante{i+1}1234");
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UsuariosInitializer] Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
