using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppMuseo.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppMuseo.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? "/";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? "/";
            
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                Console.WriteLine("El modelo o credenciales son nulos");
                ModelState.AddModelError(string.Empty, "Las credenciales son obligatorias.");
                return View(new LoginViewModel());
            }
            
            Console.WriteLine($"Intento de inicio de sesión para el usuario: {model.Email}");
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelo no válido");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                }
                return View(model);
            }

            try
            {
                Console.WriteLine("Buscando usuario...");
                var user = await _userManager.FindByEmailAsync(model.Email);
                
                if (user == null)
                {
                    Console.WriteLine($"No se encontró ningún usuario con el correo: {model.Email}");
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return View(model);
                }

                Console.WriteLine($"Usuario encontrado: {user.UserName}");
                Console.WriteLine("Intentando iniciar sesión...");
                
                if (user.UserName == null)
                {
                    Console.WriteLine("El nombre de usuario es nulo");
                    ModelState.AddModelError(string.Empty, "Error en las credenciales proporcionadas.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName, 
                    model.Password, 
                    model.RememberMe, 
                    lockoutOnFailure: true);
                    
                if (result.Succeeded)
                {
                    Console.WriteLine("Inicio de sesión exitoso");
                    return RedirectToLocal(returnUrl);
                }
                
                if (result.IsLockedOut)
                {
                    Console.WriteLine("Cuenta bloqueada temporalmente");
                    ModelState.AddModelError(string.Empty, "Cuenta bloqueada temporalmente. Intente de nuevo más tarde.");
                }
                else if (result.RequiresTwoFactor)
                {
                    Console.WriteLine("Se requiere autenticación de dos factores");
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                else if (result.IsNotAllowed)
                {
                    Console.WriteLine("No se permite el inicio de sesión");
                    ModelState.AddModelError(string.Empty, "No se permite el inicio de sesión para esta cuenta.");
                }
                else
                {
                    Console.WriteLine("Credenciales inválidas");
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al intentar iniciar sesión. Por favor, intente nuevamente.");
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
