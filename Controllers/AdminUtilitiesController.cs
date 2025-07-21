using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppMuseo.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AppMuseo.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminUtilitiesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUtilitiesController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
