using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using QRCoder;
using static QRCoder.PngByteQRCode;

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
                        "normal" => TipoEntrada.Normal,
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
                var queryGrafico = _context.Entradas.AsQueryable();
                
                // Aplicar el mismo filtro que en la consulta principal
                if (!string.IsNullOrEmpty(filtroTipo))
                {
                    TipoEntrada? tipoFiltro = filtroTipo.ToLower() switch
                    {
                        "normal" => TipoEntrada.Normal,
                        "general" => TipoEntrada.General,
                        "estudiante" => TipoEntrada.Estudiante,
                        "investigador" => TipoEntrada.Investigador,
                        "discapacidad" => TipoEntrada.Discapacidad,
                        _ => (TipoEntrada?)null
                    };
                    
                    if (tipoFiltro.HasValue)
                    {
                        queryGrafico = queryGrafico.Where(e => e.TipoEntrada == tipoFiltro.Value);
                    }
                }

                var ventasPorTipo = await queryGrafico
                    .GroupBy(e => e.TipoEntrada)
                    .Select(g => new
                    {
                        Tipo = g.Key,
                        Cantidad = g.Count(),
                        Total = g.Sum(e => e.Precio) 
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

            // Generar código QR con los datos de la entrada y la URL de validación
            string urlValidacion = $"{Request.Scheme}://{Request.Host}/Validacion/ValidarEntrada/{entrada.Id}";
            string qrText = string.Format(
                "ID: {0}{5}" +
                "Fecha: {1:dd/MM/yyyy}{5}" +
                "Hora: {2:hh\\:mm}{5}" +
                "Tipo: {3}{5}" +
                "Total: {4:C2}{5}" +
                "URL: {6}",
                entrada.Id,
                entrada.Fecha,
                entrada.Hora,
                entrada.TipoEntrada,
                entrada.Total,
                Environment.NewLine,
                urlValidacion
            );
            
            ViewBag.UrlValidacion = urlValidacion;

            // Generar código QR usando PngByteQRCode
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new PngByteQRCode(qrCodeData))
            {
                var qrCodeImageAsBytes = qrCode.GetGraphic(10, new byte[] { 0, 0, 0 }, new byte[] { 255, 255, 255 }, true);
                var qrCodeImageAsBase64 = Convert.ToBase64String(qrCodeImageAsBytes);
                ViewBag.QRCodeImage = $"data:image/png;base64,{qrCodeImageAsBase64}";
                ViewBag.QRCodeText = qrText;
            }

            return View(entrada);
        }
        // GET: Ventas/ValidarEntrada/5
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
