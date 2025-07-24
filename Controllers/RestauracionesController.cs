using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AppMuseo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppMuseo.Controllers
{
    [Authorize(Roles = "Administrador,Restaurador")]
    public class RestauracionesController : Controller
    {
        private readonly AppMuseoDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RestauracionesController(AppMuseoDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Restauraciones
        public async Task<IActionResult> Index()
        {
            var obrasEnRestauracion = await _context.Obras
                .Include(o => o.Coleccion)
                .Where(o => o.Estado == EstadoObra.EnRestauracion)
                .OrderByDescending(o => o.UltimaModificacion)
                .ToListAsync();

            return View(obrasEnRestauracion);
        }

        // GET: Restauraciones/Create
        public IActionResult Create()
        {
            ViewBag.Colecciones = new SelectList(_context.Colecciones.ToList(), "Id", "Nombre");
            return View();
        }

        // POST: Restauraciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Obra obra, IFormFile imagen)
        {
            if (ModelState.IsValid)
            {
                obra.Estado = EstadoObra.EnRestauracion;
                obra.FechaCreacion = DateTime.Now;
                obra.UltimaModificacion = DateTime.Now;
                obra.CreadoPor = User.FindFirstValue(ClaimTypes.Name);

                if (imagen != null && imagen.Length > 0)
                {
                    var fileName = Path.GetFileName(imagen.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/obras", fileName);
                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagen.CopyToAsync(fileStream);
                    }
                    
                    obra.ImageUrl = $"/images/obras/{fileName}";
                }

                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Colecciones = new SelectList(_context.Colecciones.ToList(), "Id", "Nombre", obra.ColeccionId);
            return View(obra);
        }

        // GET: Restauraciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obras.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }
            
            if (obra.Estado != EstadoObra.EnRestauracion)
            {
                return Forbid();
            }

            var colecciones = await _context.Colecciones.ToListAsync();
            ViewBag.Colecciones = new SelectList(colecciones, nameof(Coleccion.Id), nameof(Coleccion.Nombre), obra.ColeccionId);
            return View(obra);
        }

        // POST: Restauraciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Obra obra, IFormFile? imagen)
        {
            if (id != obra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var obraExistente = await _context.Obras.FindAsync(id);
                    if (obraExistente == null)
                    {
                        return NotFound();
                    }

                    if (obraExistente.Estado != EstadoObra.EnRestauracion)
                    {
                        return Forbid();
                    }

                    // Actualizar propiedades
                    obraExistente.Titulo = obra.Titulo;
                    obraExistente.Autor = obra.Autor;
                    obraExistente.Anio = obra.Anio;
                    obraExistente.UbicacionActual = obra.UbicacionActual;
                    obraExistente.ColeccionId = obra.ColeccionId;
                    obraExistente.UltimaModificacion = DateTime.Now;

                    // Manejar la imagen si se proporciona una nueva
                    if (imagen != null && imagen.Length > 0)
                    {
                        var fileName = Path.GetFileName(imagen.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/obras", fileName);
                        
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imagen.CopyToAsync(fileStream);
                        }
                        
                        obraExistente.ImageUrl = $"/images/obras/{fileName}";
                    }

                    _context.Update(obraExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObraExists(obra.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var colecciones = await _context.Colecciones.ToListAsync();
            ViewBag.Colecciones = new SelectList(colecciones, nameof(Coleccion.Id), nameof(Coleccion.Nombre), obra.ColeccionId);
            return View(obra);
        }

        // GET: Restauraciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obras
                .Include(o => o.Coleccion)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (obra == null)
            {
                return NotFound();
            }

            if (obra.Estado != EstadoObra.EnRestauracion)
            {
                return Forbid();
            }

            return View(obra);
        }

        // POST: Restauraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra != null && obra.Estado == EstadoObra.EnRestauracion)
            {
                _context.Obras.Remove(obra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Restauraciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obra = await _context.Obras
                .Include(o => o.Coleccion)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (obra == null)
            {
                return NotFound();
            }

            if (obra.Estado != EstadoObra.EnRestauracion)
            {
                return Forbid();
            }

            return View(obra);
        }

        private bool ObraExists(int id)
        {
            return _context.Obras.Any(e => e.Id == id);
        }
    }
}



