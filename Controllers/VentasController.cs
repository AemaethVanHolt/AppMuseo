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
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.Filtros = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Todas las entradas" },
                new SelectListItem { Value = "normal", Text = "Normal" },
                new SelectListItem { Value = "general", Text = "General" },
                new SelectListItem { Value = "estudiante", Text = "Estudiante" },
                new SelectListItem { Value = "investigador", Text = "Investigador" },
                new SelectListItem { Value = "discapacidad", Text = "Discapacidad" }
            };

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
                    _logger.LogInformation($"Aplicando filtro: {filtroTipo}");
                    
                    // Mapear los valores del filtro a los valores del enum TipoEntrada
                    TipoEntrada? tipoFiltro = filtroTipo.ToLower() switch
                    {
                        "general" => TipoEntrada.General,
                        "estudiante" => TipoEntrada.Estudiante,
                        "investigador" => TipoEntrada.Investigador,
                        "discapacidad" => TipoEntrada.Discapacidad,
                        _ => (TipoEntrada?)null
                    };
                    
                    if (tipoFiltro.HasValue)
                    {
                        query = query.Where(e => e.TipoEntrada == tipoFiltro.Value);
                    }
                }

                var ventas = await query
                    .OrderByDescending(e => e.Fecha)
                    .ThenByDescending(e => e.Hora)
                    .ToListAsync();

                ViewBag.FiltroSeleccionado = filtroTipo;

                // Datos para el gráfico
                var ventasPorTipo = await _context.Entradas
                    .GroupBy(e => e.TipoEntrada)
                    .Select(g => new
                    {
                        Tipo = g.Key,
                        Cantidad = g.Count(),
                        Total = g.Sum(e => e.PrecioTotal)
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .ToListAsync();

                ViewBag.Labels = ventasPorTipo.Select(x => x.Tipo).ToArray();
                ViewBag.Cantidades = ventasPorTipo.Select(x => x.Cantidad).ToArray();
                ViewBag.Totales = ventasPorTipo.Select(x => x.Total).ToArray();

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
