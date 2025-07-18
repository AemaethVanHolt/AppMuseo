using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMuseo.Controllers
{
    [Authorize]
    public class ObrasController : Controller
    {
        private readonly AppMuseoDbContext _context;

        public ObrasController(AppMuseoDbContext context)
        {
            _context = context;
        }

        // GET: Obras
        public async Task<IActionResult> Index()
        {
            var obras = await _context.Obras.Include(o => o.Coleccion).ToListAsync();
            return View(obras);
        }

        // GET: Obras/Details/5
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var obra = await _context.Obras.Include(o => o.Coleccion).FirstOrDefaultAsync(m => m.Id == id);
            if (obra == null) return NotFound();
            return View(obra);
        }

        // GET: Obras/Create
        [Authorize(Roles="Administrador,Restaurador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Obras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador,Restaurador")]
        public async Task<IActionResult> Create([Bind("ImageUrl,Titulo,Autor,Anio,Estado,UbicacionActual,ColeccionId")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                obra.FechaCreacion = DateTime.Now;
                obra.UltimaModificacion = DateTime.Now;
                obra.CreadoPor = User.Identity.Name;
                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }

        // GET: Obras/Edit/5
        [Authorize(Roles="Administrador,Restaurador")]
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();
            return View(obra);
        }

        // POST: Obras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador,Restaurador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,Titulo,Autor,Anio,Estado,UbicacionActual,ColeccionId")] Obra obra)
        {
            if (id != obra.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    obra.UltimaModificacion = DateTime.Now;
                    _context.Update(obra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Obras.Any(e => e.Id == obra.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }

        // GET: Obras/Delete/5
        [Authorize(Roles="Administrador")]
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var obra = await _context.Obras.Include(o => o.Coleccion).FirstOrDefaultAsync(m => m.Id == id);
            if (obra == null) return NotFound();
            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra != null)
            {
                _context.Obras.Remove(obra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
