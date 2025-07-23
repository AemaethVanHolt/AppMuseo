using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;

namespace AppMuseo.Controllers
{
    [Authorize(Roles = "Administrador,Empleado")]
    public class ValidacionController : Controller
    {
        private readonly AppMuseoDbContext _context;

        public ValidacionController(AppMuseoDbContext context)
        {
            _context = context;
        }

        // GET: Validacion
        public IActionResult Index()
        {
            return View();
        }

        // GET: Validacion/Escaner
        public IActionResult Escaner()
        {
            return View();
        }

        // GET: Validacion/ValidarEntrada/5
        [HttpGet]
        public async Task<IActionResult> ValidarEntrada(int id)
        {
            var entrada = await _context.Entradas
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entrada == null)
            {
                return Json(new { success = false, message = "Entrada no encontrada." });
            }

            // Verificar si la entrada ya ha sido utilizada
            if (entrada.Utilizada)
            {
                return Json(new { 
                    success = false, 
                    message = "Esta entrada ya ha sido utilizada.",
                    entrada = new {
                        id = entrada.Id,
                        tipo = entrada.TipoEntrada.ToString(),
                        fecha = entrada.Fecha.ToString("dd/MM/yyyy"),
                        hora = entrada.Hora.ToString(),
                        precio = entrada.Total,
                        utilizada = entrada.Utilizada
                    }
                });
            }

            // Marcar la entrada como utilizada
            entrada.Utilizada = true;
            entrada.FechaUso = DateTime.Now;
            _context.Update(entrada);
            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                message = "Entrada validada correctamente.",
                entrada = new {
                    id = entrada.Id,
                    tipo = entrada.TipoEntrada.ToString(),
                    fecha = entrada.Fecha.ToString("dd/MM/yyyy"),
                    hora = entrada.Hora.ToString(),
                    precio = entrada.Total,
                    utilizada = true
                }
            });
        }
    }
}
