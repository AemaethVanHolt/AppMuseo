using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AppMuseo.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // 1. Crear roles y admin (solo uno)
            await SeedData.InitializeAsync(serviceProvider);
            // 2. Crear usuarios adicionales (restaurador, visitantes)
            await UsuariosInitializer.SeedUsuariosAsync(serviceProvider);
            // 3. Crear colecciones
            await ColeccionInitializer.SeedColeccionesAsync(serviceProvider);
            // 4. Crear obras
            await ObraInitializer.SeedObrasAsync(serviceProvider);
            // 5. Crear entradas para los visitantes
            await EntradasInitializer.SeedEntradasAsync(serviceProvider);
        }
    }
}
