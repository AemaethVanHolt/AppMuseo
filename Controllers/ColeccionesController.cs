using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMuseo.Controllers
{
    [Authorize]
    public class ColeccionesController : Controller
    {
        private readonly AppMuseoDbContext _context;

        public ColeccionesController(AppMuseoDbContext context)
        {
            _context = context;
        }

        // GET: Colecciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colecciones.Include(c => c.Obras).ToListAsync());
        }

        // GET: Colecciones/Details/5
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var coleccion = await _context.Colecciones.Include(c => c.Obras).FirstOrDefaultAsync(m => m.Id == id);
            if (coleccion == null) return NotFound();
            return View(coleccion);
        }

        // GET: Colecciones/Create
        [Authorize(Roles="Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colecciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador")]
        public async Task<IActionResult> Create([Bind("Nombre,Tipo,Descripcion")] Coleccion coleccion)
        {
            if (ModelState.IsValid)
            {
                coleccion.FechaCreacion = DateTime.Now;
                coleccion.UltimaModificacion = DateTime.Now;
                coleccion.CreadoPor = User.Identity.Name;
                _context.Add(coleccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coleccion);
        }

        // GET: Colecciones/Edit/5
        [Authorize(Roles="Administrador")]
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var coleccion = await _context.Colecciones.FindAsync(id);
            if (coleccion == null) return NotFound();
            return View(coleccion);
        }

        // POST: Colecciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Tipo,Descripcion")] Coleccion coleccion)
        {
            if (id != coleccion.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    coleccion.UltimaModificacion = DateTime.Now;
                    _context.Update(coleccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Colecciones.Any(e => e.Id == coleccion.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coleccion);
        }

        // GET: Colecciones/Delete/5
        [Authorize(Roles="Administrador")]
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var coleccion = await _context.Colecciones.Include(c => c.Obras).FirstOrDefaultAsync(m => m.Id == id);
            if (coleccion == null) return NotFound();
            return View(coleccion);
        }

        // POST: Colecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coleccion = await _context.Colecciones.FindAsync(id);
            if (coleccion != null)
            {
                _context.Colecciones.Remove(coleccion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
