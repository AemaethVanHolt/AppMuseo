using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using AppMuseo.Models;

namespace AppMuseo.Controllers
{
    [Authorize] // Solo usuarios autenticados pueden acceder
    public class TaquillaController : Controller
    {
        private readonly ILogger<TaquillaController> _logger;

        public TaquillaController(ILogger<TaquillaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Aquí irá la lógica para mostrar las entradas disponibles
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcesarCompra(string tipoEntrada, decimal precio, List<string> extras = null)
        {
            // Crear modelo para la vista de resumen
            var model = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "PrecioUnitario", precio },
                { "ExtrasDisponibles", new Dictionary<string, decimal>
                    {
                        { "Audioguía", 2.50m },
                        { "Visita Guiada", 4.00m },
                        { "Autorización Fotográfica", 1.50m }
                    }
                },
                { "ExtrasSeleccionados", extras ?? new List<string>() }
            };

            // Pasar a la vista de resumen
            return View("~/Views/Compra/Resumen.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarCompra(string tipoEntrada, decimal precioUnitario, int cantidad, List<string> extras)
        {
            if (cantidad <= 0)
            {
                return RedirectToAction("Index");
            }

            // Calcular total
            decimal total = precioUnitario * cantidad;
            
            // Sumar el costo de los extras
            if (extras != null && extras.Any())
            {
                total += extras.Sum(extra => 
                    extra switch
                    {
                        "Audioguía" => 2.50m * cantidad,
                        "Visita Guiada" => 4.00m * cantidad,
                        "Autorización Fotográfica" => 1.50m * cantidad,
                        _ => 0m
                    });
            }

            // Aquí iría la lógica para guardar la compra en la base de datos
            // Por ahora, solo redirigimos a la página de confirmación
            
            var model = new Dictionary<string, object>
            {
                { "TipoEntrada", tipoEntrada },
                { "PrecioUnitario", precioUnitario },
                { "Cantidad", cantidad },
                { "Total", total },
                { "Extras", extras ?? new List<string>() },
                { "NumeroConfirmacion", Guid.NewGuid().ToString().Substring(0, 8).ToUpper() }
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
