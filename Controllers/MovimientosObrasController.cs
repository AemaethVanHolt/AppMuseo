using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMuseo.Controllers
{
    [Authorize(Roles="Administrador,Restaurador")]
    public class MovimientosObrasController : Controller
    {
        private readonly AppMuseoDbContext _context;

        public MovimientosObrasController(AppMuseoDbContext context)
        {
            _context = context;
        }

        // GET: MovimientosObras
        public async Task<IActionResult> Index()
        {
            var movimientos = await _context.MovimientosObras.Include(m => m.Obra).ToListAsync();
            return View(movimientos);
        }

        // GET: MovimientosObras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var movimiento = await _context.MovimientosObras.Include(m => m.Obra).FirstOrDefaultAsync(m => m.Id == id);
            if (movimiento == null) return NotFound();
            return View(movimiento);
        }

        // GET: MovimientosObras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovimientosObras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObraId,FechaMovimiento,TipoMovimiento,UbicacionDestino,Observaciones")] MovimientoObra movimientoObra)
        {
            if (ModelState.IsValid)
            {
                movimientoObra.FechaCreacion = DateTime.Now;
                movimientoObra.UltimaModificacion = DateTime.Now;
                movimientoObra.CreadoPor = User.Identity.Name;
                _context.Add(movimientoObra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimientoObra);
        }

        // GET: MovimientosObras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var movimientoObra = await _context.MovimientosObras.FindAsync(id);
            if (movimientoObra == null) return NotFound();
            return View(movimientoObra);
        }

        // POST: MovimientosObras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ObraId,FechaMovimiento,TipoMovimiento,UbicacionDestino,Observaciones")] MovimientoObra movimientoObra)
        {
            if (id != movimientoObra.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    movimientoObra.UltimaModificacion = DateTime.Now;
                    _context.Update(movimientoObra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MovimientosObras.Any(e => e.Id == movimientoObra.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movimientoObra);
        }

        // GET: MovimientosObras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var movimientoObra = await _context.MovimientosObras.Include(m => m.Obra).FirstOrDefaultAsync(m => m.Id == id);
            if (movimientoObra == null) return NotFound();
            return View(movimientoObra);
        }

        // POST: MovimientosObras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimientoObra = await _context.MovimientosObras.FindAsync(id);
            if (movimientoObra != null)
            {
                _context.MovimientosObras.Remove(movimientoObra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
