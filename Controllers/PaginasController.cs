using Microsoft.AspNetCore.Mvc;
using AppMuseo.Models.ViewModels;
using System;
using Microsoft.Extensions.Logging;

namespace AppMuseo.Controllers
{
    public class PaginasController : Controller
    {
        // Páginas de información general
        [Route("Paginas/GuiaMuseo")]
        public IActionResult GuiaMuseo()
        {
            return View("~/Views/Paginas/GuiaMuseo.cshtml");
        }

        [Route("Paginas/HistoriaMuseo")]
        public IActionResult HistoriaMuseo()
        {
            return View("~/Views/Paginas/HistoriaMuseo.cshtml");
        }

        // Páginas de visitas
        [Route("Paginas/VisitasGuiadas")]
        public IActionResult VisitasGuiadas()
        {
            return View("~/Views/Paginas/VisitasGuiadas.cshtml");
        }

        [Route("Paginas/VisitasGrupo")]
        public IActionResult VisitasGrupo()
        {
            return View("~/Views/Paginas/VisitasGrupo.cshtml");
        }

        // Páginas de exposiciones
        [Route("Paginas/Exposiciones")]
        public IActionResult Exposiciones()
        {
            return View("~/Views/Paginas/Exposiciones.cshtml");
        }

        // Páginas de restauración
        [Route("Paginas/TallerRestauracion")]
        public IActionResult TallerRestauracion()
        {
            return View("~/Views/Paginas/TallerRestauracion.cshtml");
        }

        [Route("Paginas/ReservarEntrada")]
        public IActionResult ReservarEntrada(int id, string titulo, string artista, string fecha, string descripcion, string ubicacion, string imagen)
        {
            var modelo = new ReservarEntradaViewModel
            {
                Id = id,
                Titulo = titulo,
                Artista = artista,
                Fecha = fecha,
                Descripcion = descripcion,
                Ubicacion = ubicacion,
                Imagen = imagen
            };

            return View("~/Views/Paginas/ReservarEntrada.cshtml", modelo);
        }

        [HttpPost]
        [Route("Paginas/ReservarEntrada")]
        [ValidateAntiForgeryToken]
        public IActionResult ReservarEntrada(ReservarEntradaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                // Aquí iría la lógica para procesar la reserva
                // Por ahora, redirigimos a la página de confirmación
                return RedirectToAction("ConfirmacionReserva", new { id = modelo.Id });
            }

            return View("~/Views/Paginas/ReservarEntrada.cshtml", modelo);
        }

        [Route("Paginas/ConfirmacionReserva/{id}")]
        public IActionResult ConfirmacionReserva(int id)
        {
            // Aquí podrías obtener los detalles de la reserva de la base de datos
            // Por ahora, simplemente mostramos la vista de confirmación
            return View("~/Views/Paginas/ConfirmacionReserva.cshtml", id);
        }

        [Route("Paginas/ReservaVisitaGuiada")]
        public IActionResult ReservaVisitaGuiada()
        {
            var modelo = new ReservaVisitaGuiadaViewModel();
            return View("~/Views/Paginas/ReservaVisitaGuiada.cshtml", modelo);
        }

        [HttpPost]
        [Route("Paginas/ReservaVisitaGuiada")]
        [ValidateAntiForgeryToken]
        public IActionResult ReservaVisitaGuiada(ReservaVisitaGuiadaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Aquí iría la lógica para guardar la reserva en la base de datos
                    // Por ejemplo:
                    // _reservaService.GuardarReservaVisitaGuiada(modelo);
                    
                    // Enviar correo de confirmación
                    // _emailService.EnviarConfirmacionReserva(modelo.Email, modelo);
                    
                    // Mostrar mensaje de éxito
                    ViewBag.MensajeExito = "¡Solicitud enviada con éxito!";
                    
                    // Limpiar el modelo para un nuevo formulario
                    ModelState.Clear();
                    var nuevoModelo = new ReservaVisitaGuiadaViewModel();
                    return View("~/Views/Paginas/ReservaVisitaGuiada.cshtml", nuevoModelo);
                }
                catch (Exception ex)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "No se pudo procesar la solicitud. Por favor, inténtelo de nuevo más tarde.");
                    // Log the error details
                    // _logger.LogError(ex, "Error al procesar la reserva de visita guiada");
                }
            }
            
            // Si llegamos hasta aquí, algo salió mal, volvemos a mostrar el formulario
            return View("~/Views/Paginas/ReservaVisitaGuiada.cshtml", modelo);
        }
    }
}
