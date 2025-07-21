using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using AppMuseo.Models;

namespace AppMuseo.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Administrador", "Restaurador", "Visitante" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Crear usuario admin si no existe
            var adminEmail = "admin@museo.com";
            var adminPass = "admin1234"; // CONTRASEÑA. NO TOCAR HOSTIA!!!
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nombre = "Administrador",
                    Apellidos = "Principal",
                    FechaCreacion = DateTime.Now,
                    FechaNacimiento = DateTime.Now.AddYears(-30),
                    Activo = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPass);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");
                }
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SeedData] Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}
