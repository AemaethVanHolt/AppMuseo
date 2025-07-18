using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMuseo.Controllers
{
    [Authorize]
    public class EntradasController : Controller
    {
        private readonly AppMuseoDbContext _context;

        public EntradasController(AppMuseoDbContext context)
        {
            _context = context;
        }

        // GET: Entradas
        public async Task<IActionResult> Index()
        {
            var entradas = await _context.Entradas.Include(e => e.User).ToListAsync();
            return View(entradas);
        }

        // GET: Entradas/Details/5
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var entrada = await _context.Entradas.Include(e => e.User).FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null) return NotFound();
            return View(entrada);
        }

        // GET: Entradas/Create
        [Authorize(Roles="Visitante,Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entradas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Visitante,Administrador")]
        public async Task<IActionResult> Create([Bind("Fecha,Hora,Precio,TipoEntrada,IncluyeAutorizacionFoto,IncluyeVisitaTaller,IncluyeAudioGuia,IncluyeVisitaGuiada,Total,UserId")] Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                entrada.FechaCreacion = DateTime.Now;
                entrada.UltimaModificacion = DateTime.Now;
                entrada.CreadoPor = User.Identity.Name;
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entrada);
        }

        // GET: Entradas/Edit/5
        [Authorize(Roles="Visitante,Administrador")]
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada == null) return NotFound();
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Visitante,Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Hora,Precio,TipoEntrada,IncluyeAutorizacionFoto,IncluyeVisitaTaller,IncluyeAudioGuia,IncluyeVisitaGuiada,Total,UserId")] Entrada entrada)
        {
            if (id != entrada.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    entrada.UltimaModificacion = DateTime.Now;
                    _context.Update(entrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Entradas.Any(e => e.Id == entrada.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entrada);
        }

        // GET: Entradas/Delete/5
        [Authorize(Roles="Administrador")]
        // Null checks already present to avoid CS8602
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var entrada = await _context.Entradas.Include(e => e.User).FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null) return NotFound();
            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
