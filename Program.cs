using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Models;
using AppMuseo.Data;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppMuseoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.MigrationsAssembly("AppMuseo")));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ReturnUrlParameter = "returnUrl";
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<AppMuseoDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configurar el middleware para servir archivos estáticos
app.UseStaticFiles();

// Configurar el enrutamiento
app.UseRouting();

// Configurar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Aplicar migraciones pendientes y seed inicial
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppMuseoDbContext>();
        
        // Aplicar migraciones pendientes
        Console.WriteLine("Aplicando migraciones pendientes...");
        await context.Database.MigrateAsync();
        Console.WriteLine("Migraciones aplicadas correctamente.");
        
        // Ejecutar seed inicial completo
        Console.WriteLine("Inicializando datos...");
        await DbInitializer.InitializeAsync(services);
        
        Console.WriteLine("Datos inicializados correctamente.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar la base de datos.");
        Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
        ctx.Context.Response.Headers.Append("Expires", "0");
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configurar rutas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
