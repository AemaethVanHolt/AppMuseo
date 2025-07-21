using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppMuseo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMuseo.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string roleFilter = "")
        {
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, IList<string>>();
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            
            // Aplicar filtro si se especificó
            if (!string.IsNullOrEmpty(roleFilter) && roleFilter != "Todos")
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleFilter);
                var userIdsInRole = usersInRole.Select(u => u.Id).ToHashSet();
                users = users.Where(u => userIdsInRole.Contains(u.Id)).ToList();
            }

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles;
            }

            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.SelectedRole = roleFilter;
            
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Activo = !user.Activo;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Si se desactiva el usuario, forzar cierre de sesión
                if (!user.Activo)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                TempData["SuccessMessage"] = $"Estado del usuario {user.UserName} actualizado correctamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar el estado del usuario.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
