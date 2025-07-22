using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using AppMuseo.Models;

namespace AppMuseo.Controllers
{
    public class TaquillaController : Controller
    {
        private readonly ILogger<TaquillaController> _logger;

        public TaquillaController(ILogger<TaquillaController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // Aquí irá la lógica para mostrar las entradas disponibles
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous] // No requiere autenticación para ver el resumen
        public IActionResult ProcesarCompra(string tipoEntrada, string precio, string precioTotal, List<string> extras = null)
        {
            // Asegurarse de que el precio tenga el formato correcto
            if (!decimal.TryParse(precio.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal precioBase))
            {
                // Si no se puede parsear, usar un valor por defecto según el tipo de entrada
                precioBase = tipoEntrada.ToLower() switch
                {
                    "entrada general" => 15.00m,
                    "entrada familiar" => 35.00m,
                    "entrada reducida" => 7.50m,
                    _ => 0m
                };
            }
            
            // Obtener el precio total del formulario si está disponible
            decimal precioTotalDecimal = precioBase;
            if (!string.IsNullOrEmpty(precioTotal) && decimal.TryParse(precioTotal.Replace(",", "."), 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.CultureInfo.InvariantCulture, 
                out decimal precioTotalParsed))
            {
                precioTotalDecimal = precioTotalParsed;
            }

            // Mapear los valores de los extras a sus nombres legibles
            var extrasNombres = new List<string>();
            if (extras != null)
            {
                foreach (var extra in extras)
                {
                    switch (extra)
                    {
                        case "audioguia":
                            extrasNombres.Add("Audioguía");
                            break;
                        case "visita":
                            extrasNombres.Add("Visita Guiada");
                            break;
                        case "foto":
                            extrasNombres.Add("Autorización Fotográfica");
                            break;
                    }
                }
            }

            // Crear modelo para la vista de resumen
            var model = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "PrecioBase", precioBase },
                { "PrecioTotal", precioTotalDecimal },
                { "ExtrasDisponibles", new Dictionary<string, decimal>
                    {
                        { "Audioguía", 2.50m },
                        { "Visita Guiada", 4.00m },
                        { "Autorización Fotográfica", 1.50m }
                    }
                },
                { "ExtrasSeleccionados", extrasNombres }
            };

            // Pasar a la vista de resumen
            return View("~/Views/Compra/Resumen.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Requiere autenticación SOLO para confirmar la compra
        public IActionResult ConfirmarCompra(string tipoEntrada, decimal precioUnitario, decimal precioTotal, int cantidad, List<string> extras)
        {
            if (cantidad <= 0)
            {
                return RedirectToAction("Index");
            }

            // Mapear los valores de los extras a sus nombres legibles
            var extrasNombres = new List<string>();
            if (extras != null)
            {
                foreach (var extra in extras)
                {
                    switch (extra)
                    {
                        case "audioguia":
                            extrasNombres.Add("Audioguía");
                            break;
                        case "visita":
                            extrasNombres.Add("Visita Guiada");
                            break;
                        case "foto":
                            extrasNombres.Add("Autorización Fotográfica");
                            break;
                    }
                }
            }

            // Aquí iría la lógica para guardar la compra en la base de datos
            // Por ahora, solo redirigimos a la página de confirmación
            
            var model = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "PrecioUnitario", precioUnitario },
                { "PrecioTotal", precioTotal },
                { "Cantidad", cantidad },
                { "Total", precioTotal },
                { "Extras", extrasNombres },
                { "NumeroConfirmacion", Guid.NewGuid().ToString().Substring(0, 8).ToUpper() },
                { "FechaCompra", DateTime.Now.ToString("dd/MM/yyyy HH:mm") }
            };

            return View("~/Views/Compra/Confirmacion.cshtml", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
