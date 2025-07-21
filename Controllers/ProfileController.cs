using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppMuseo.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppMuseo.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment env,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id = null)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            ApplicationUser user;
            bool isAdmin = await _userManager.IsInRoleAsync(currentUser, "Administrador");
            
            if (!string.IsNullOrEmpty(id) && isAdmin)
            {
                // Si es admin y se proporciona un ID, editar ese usuario
                user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
            }
            else
            {
                // De lo contrario, editar perfil propio
                user = currentUser;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            var esMismoUsuario = user.Id == currentUser.Id;
            var esAdminActual = await _userManager.IsInRoleAsync(user, "Administrador");

            var model = new EditProfileViewModel
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Email = user.Email,
                Telefono = user.PhoneNumber,
                Direccion = user.Direccion,
                Pais = user.Pais,
                FechaNacimiento = user.FechaNacimiento,
                FechaCreacion = user.FechaCreacion,
                UltimoAcceso = user.LockoutEnd?.DateTime,
                FotoPerfilUrl = user.FotoPerfil,
                Rol = userRoles.FirstOrDefault(),
                RolesDisponibles = allRoles.Where(r => r != null).ToList(),
                Activo = user.Activo,
                EsAdminActual = esAdminActual,
                EsMismoUsuario = esMismoUsuario,
                PuedeDesactivar = isAdmin && !esMismoUsuario && !esAdminActual
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            bool isAdmin = await _userManager.IsInRoleAsync(currentUser, "Administrador");
            bool esMismoUsuario = model.Id == currentUser.Id;
            
            // Obtener el usuario a editar
            ApplicationUser user;
            if (!string.IsNullOrEmpty(model.Id) && isAdmin)
            {
                user = await _userManager.FindByIdAsync(model.Id) ?? throw new InvalidOperationException("Usuario no encontrado");
                if (user == null)
                {
                    return NotFound();
                }
            }
            else
            {
                user = currentUser;
                model.Id = currentUser.Id; // Asegurarse de que el ID sea el correcto
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            bool esAdminActual = await _userManager.IsInRoleAsync(user, "Administrador");
            
            // Validar que un administrador no pueda cambiar su propio rol
            if (esMismoUsuario && esAdminActual && !string.IsNullOrEmpty(model.Rol) && model.Rol != "Administrador")
            {
                ModelState.AddModelError(string.Empty, "No puedes cambiar tu propio rol de Administrador.");
                model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
                model.EsAdminActual = esAdminActual;
                model.EsMismoUsuario = esMismoUsuario;
                model.PuedeDesactivar = isAdmin && !esMismoUsuario && !esAdminActual;
                return View(model);
            }

            // Validar que un administrador no pueda desactivarse a sí mismo
            if (esMismoUsuario && esAdminActual)
            {
                // Forzar el estado a activo para el administrador actual
                model.Activo = true;
                
                // Si se está intentando desactivar, mostrar error
                if (!model.Activo && user.Activo)
                {
                    ModelState.AddModelError(string.Empty, "No puedes desactivar tu propia cuenta de administrador.");
                    model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
                    model.EsAdminActual = esAdminActual;
                    model.EsMismoUsuario = esMismoUsuario;
                    model.PuedeDesactivar = isAdmin && !esMismoUsuario && !esAdminActual;
                    return View(model);
                }
            }

            // Actualizar propiedades básicas
            user.Nombre = model.Nombre;
            user.Apellidos = model.Apellidos;
            user.PhoneNumber = model.Telefono;
            user.Direccion = model.Direccion;
            user.Pais = model.Pais;
            user.FechaNacimiento = model.FechaNacimiento ?? user.FechaNacimiento;

            // Actualizar el correo electrónico si es administrador o si está editando su propio perfil
            if (isAdmin || user.Id == currentUser.Id)
            {
                var email = await _userManager.GetEmailAsync(user);
                if (model.Email != email)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        foreach (var error in setEmailResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
                        model.EsAdminActual = await _userManager.IsInRoleAsync(user, "Administrador");
                        model.EsMismoUsuario = user.Id == currentUser.Id;
                        model.PuedeDesactivar = isAdmin && user.Id != currentUser.Id;
                        return View(model);
                    }
                }
            }

            // Actualizar el rol si es administrador y no es su propio perfil
            if (isAdmin && user.Id != currentUser.Id && !string.IsNullOrEmpty(model.Rol))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, model.Rol);
            }

            // Actualizar el estado activo si es administrador y no es su propio perfil
            if (isAdmin && user.Id != currentUser.Id)
            {
                user.Activo = model.Activo;
                
                // Si se desactiva el usuario, forzar cierre de sesión
                if (!model.Activo)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    // Refrescar la sesión del usuario actual
                    var loggedInUser = await _userManager.GetUserAsync(User);
                    if (loggedInUser != null)
                    {
                        await _signInManager.RefreshSignInAsync(loggedInUser);
                    }
                }
            }

            // Actualizar la foto de perfil si se proporciona
            if (model.FotoPerfil != null && model.FotoPerfil.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "perfiles");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.FotoPerfil.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.FotoPerfil.CopyToAsync(fileStream);
                }

                // Eliminar la foto anterior si existe
                if (!string.IsNullOrEmpty(user.FotoPerfil))
                {
                    var oldFilePath = Path.Combine(_env.WebRootPath, user.FotoPerfil.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                user.FotoPerfil = $"/uploads/perfiles/{uniqueFileName}";
            }

            // Actualizar la contraseña si se proporciona
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                if (!string.IsNullOrEmpty(model.CurrentPassword))
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
                        model.EsAdminActual = await _userManager.IsInRoleAsync(user, "Administrador");
                        model.EsMismoUsuario = user.Id == currentUser.Id;
                        model.PuedeDesactivar = isAdmin && user.Id != currentUser.Id;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Debe proporcionar la contraseña actual para cambiarla");
                    model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
                    model.EsAdminActual = await _userManager.IsInRoleAsync(user, "Administrador");
                    model.EsMismoUsuario = user.Id == currentUser.Id;
                    model.PuedeDesactivar = isAdmin && user.Id != currentUser.Id;
                    return View(model);
                }
            }

            // Guardar los cambios del usuario
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
                model.EsAdminActual = await _userManager.IsInRoleAsync(user, "Administrador");
                model.EsMismoUsuario = user.Id == currentUser.Id;
                model.PuedeDesactivar = isAdmin && user.Id != currentUser.Id;
                return View(model);
            }

            // Si el usuario está editando su propio perfil, actualizar la cookie de autenticación
            if (user.Id == currentUser.Id)
            {
                await _signInManager.RefreshSignInAsync(user);
            }

            model.RolesDisponibles = _roleManager.Roles.Where(r => r.Name != null).Select(r => r.Name!).ToList();
            return View(model);
        }
    }
}
