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
        public async Task<IActionResult> Index()
        {
            try
            {
                var ventas = await _context.Entradas
                    .Include(e => e.User)
                    .Include(e => e.Descuento)
                    .Include(e => e.Extra)
                    .OrderByDescending(e => e.Fecha)
                    .ThenByDescending(e => e.Hora)
                    .ToListAsync();

                return View(ventas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de ventas");
                ModelState.AddModelError("", "Ocurrió un error al cargar las ventas. Por favor, inténtelo de nuevo más tarde.");
                return View(new List<Entrada>());
            }
        }
    }
}
