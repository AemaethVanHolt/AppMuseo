using Microsoft.AspNetCore.Mvc;

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
    }
}
