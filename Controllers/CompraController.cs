using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppMuseo.Controllers
{
    public class CompraController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Resumen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirmar()
        {
            // Aquí iría la lógica para procesar el pago
            // Por ahora, solo redirigimos a una vista de confirmación
            return View("Confirmacion");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
