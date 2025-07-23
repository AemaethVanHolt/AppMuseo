using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Models;

namespace AppMuseo.Data
{
    public class AppMuseoDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppMuseoDbContext(DbContextOptions<AppMuseoDbContext> options) : base(options) { }

        public DbSet<Obra> Obras { get; set; }
        public DbSet<Coleccion> Colecciones { get; set; }
        public DbSet<MovimientoObra> MovimientosObras { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<VisitanteData> Visitantes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Relaciones y configuraciones personalizadas aquí
        }
    }
}
