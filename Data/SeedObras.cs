using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AppMuseo.Data
{
    public static class SeedObras
    {
        public static async Task Main()
        {
            try
            {
                Console.WriteLine("Inicializando datos de obras...");
                
                // Configurar el proveedor de servicios
                var services = new ServiceCollection();
                services.AddDbContext<AppMuseoDbContext>(options =>
                    options.UseSqlServer("Server=localhost;Database=AppMuseo;User Id=sa;Password=sa;TrustServerCertificate=True;"));
                
                var serviceProvider = services.BuildServiceProvider();
                
                // Ejecutar el inicializador de obras
                await ObraInitializer.SeedObrasAsync(serviceProvider);
                
                Console.WriteLine("¡Datos de obras inicializados correctamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar los datos de obras: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
