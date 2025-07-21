using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AppMuseo.Controllers
{
    [Authorize(Roles = "Administrador")] 
    public class VentasController : CompraController
    {
        private readonly AppMuseoDbContext _context;
        private readonly ILogger<VentasController> _logger;

        public VentasController(AppMuseoDbContext context, ILogger<VentasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Ventas
        public async Task<IActionResult> Index(string filtroTipo = "")
        {
            try
            {
                var query = _context.Entradas
                    .Include(e => e.User)
                    .Include(e => e.Descuento)
                    .Include(e => e.Extra)
                    .AsQueryable();

                // Aplicar filtros si se especifican
                if (!string.IsNullOrEmpty(filtroTipo))
                {
                    switch (filtroTipo.ToLower())
                    {
                        case "general":
                            query = query.Where(e => e.Descuento == null || 
                                (!e.Descuento.Estudiante && !e.Descuento.Investigador && !e.Descuento.Discapacidad && 
                                 !e.Descuento.TerceraEdad && !e.Descuento.FamiliaNumerosa && !e.Descuento.Desempleado));
                            break;
                        case "estudiante":
                            query = query.Where(e => e.Descuento != null && e.Descuento.Estudiante);
                            break;
                        case "investigador":
                            query = query.Where(e => e.Descuento != null && e.Descuento.Investigador);
                            break;
                        case "discapacidad":
                            query = query.Where(e => e.Descuento != null && e.Descuento.Discapacidad);
                            break;
                        case "tercera_edad":
                            query = query.Where(e => e.Descuento != null && e.Descuento.TerceraEdad);
                            break;
                        case "familia_numerosa":
                            query = query.Where(e => e.Descuento != null && e.Descuento.FamiliaNumerosa);
                            break;
                        case "desempleado":
                            query = query.Where(e => e.Descuento != null && e.Descuento.Desempleado);
                            break;
                    }
                }

                var ventas = await query
                    .OrderByDescending(e => e.Fecha)
                    .ThenByDescending(e => e.Hora)
                    .ToListAsync();

                // Pasar la lista de filtros a la vista
                ViewBag.Filtros = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "Todas las entradas" },
                    new SelectListItem { Value = "general", Text = "Entrada General" },
                    new SelectListItem { Value = "estudiante", Text = "Estudiantes" },
                    new SelectListItem { Value = "investigador", Text = "Investigadores" },
                    new SelectListItem { Value = "discapacidad", Text = "Personas con discapacidad" },
                    new SelectListItem { Value = "tercera_edad", Text = "Mayores de 65 años" },
                    new SelectListItem { Value = "familia_numerosa", Text = "Familias numerosas" },
                    new SelectListItem { Value = "desempleado", Text = "Personas desempleadas" }
                };

                ViewBag.FiltroSeleccionado = filtroTipo;
                return View(ventas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de ventas");
                ModelState.AddModelError("", "Ocurrió un error al cargar las ventas. Por favor, inténtelo de nuevo más tarde.");
                return View(new List<Entrada>());
            }
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .Include(e => e.User)
                .Include(e => e.Descuento)
                .Include(e => e.Extra)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }
    }
}
