using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppMuseo.Models;
using AppMuseo.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppMuseo.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminUtilitiesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppMuseoDbContext _context;

        public IActionResult Index()
        {
            return View();
        }

        public AdminUtilitiesController(
            UserManager<ApplicationUser> userManager,
            AppMuseoDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivarTodosUsuarios()
        {
            var users = _userManager.Users.ToList();
            int activatedCount = 0;

            foreach (var user in users)
            {
                if (!user.Activo)
                {
                    user.Activo = true;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        activatedCount++;
                    }
                }
            }

            TempData["SuccessMessage"] = $"Se han activado {activatedCount} usuarios.";
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReiniciarBaseDatos()
        {
            try
            {
                // Eliminar la base de datos existente
                await _context.Database.EnsureDeletedAsync();
                
                // Volver a crear la base de datos y aplicar migraciones
                await _context.Database.MigrateAsync();
                
                // Ejecutar el seed inicial
                await DbInitializer.InitializeAsync(HttpContext.RequestServices);
                
                TempData["SuccessMessage"] = "Base de datos reinicializada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al reinicializar la base de datos: {ex.Message}";
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
