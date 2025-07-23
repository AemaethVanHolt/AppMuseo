using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMuseo.Data;
using AppMuseo.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppMuseo.Controllers
{
    public class ColeccionesController : Controller
    {
        private readonly AppMuseoDbContext _context;

        public ColeccionesController(AppMuseoDbContext context)
        {
            _context = context;
        }

        // GET: Colecciones
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colecciones.Include(c => c.Obras).ToListAsync());
        }

        // GET: Colecciones/Details/5
        // Null checks already present to avoid CS8602
        [AllowAnonymous]
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
                coleccion.CreadoPor = User.Identity?.Name ?? "Sistema";
                _context.Add(coleccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coleccion);
        }

        // GET: Colecciones/Edit/5
        [Authorize(Roles="Administrador")]
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

        private bool ColeccionExists(int id)
        {
            return _context.Colecciones.Any(e => e.Id == id);
        }

        // Acciones para las vistas específicas de colección
        
        // GET: Colecciones/ArteModerno
        [AllowAnonymous]
        public IActionResult ArteModerno()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Arte Moderno");

            if (coleccion == null)
            {
                // Si no existe, crea una colección temporal
                coleccion = new Coleccion
                {
                    Nombre = "Arte Moderno",
                    Descripcion = "Explora las obras más representativas del arte moderno del siglo XX.",
                    Obras = new List<Obra>()
                };
            }

            return View(coleccion);
        }

        // GET: Colecciones/Renacimiento
        [AllowAnonymous]
        public IActionResult Renacimiento()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Renacimiento");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Renacimiento",
                    Descripcion = "Descubre las obras maestras del Renacimiento, un período de renacimiento cultural en Europa que abarcó los siglos XIV al XVII.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }

        // GET: Colecciones/ArteAsiatico
        [AllowAnonymous]
        public IActionResult ArteAsiatico()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Arte Asiático");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Arte Asiático",
                    Descripcion = "Explora la rica tradición artística de Asia, desde la antigüedad hasta la época contemporánea.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }

        // GET: Colecciones/PintoresFlamencos
        [AllowAnonymous]
        public IActionResult PintoresFlamencos()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Pintores Flamencos");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Pintores Flamencos",
                    Descripcion = "Admira las obras maestras de los pintores flamencos de los siglos XV al XVII, conocidos por su detallismo y uso del color.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }

        // GET: Colecciones/PintoresSigloXIX
        [AllowAnonymous]
        public IActionResult PintoresSigloXIX()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Pintores del Siglo XIX");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Pintores del Siglo XIX",
                    Descripcion = "Descubre la evolución del arte en el siglo XIX, desde el Neoclasicismo hasta el Postimpresionismo.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }

        // GET: Colecciones/PintoresRenacentistas
        [AllowAnonymous]
        public IActionResult PintoresRenacentistas()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Pintores Renacentistas");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Pintores Renacentistas",
                    Descripcion = "Explora las obras maestras de los grandes maestros del Renacimiento que revolucionaron el arte occidental.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }

        // GET: Colecciones/ColeccionTemporal
        [AllowAnonymous]
        public IActionResult ColeccionTemporal()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Colección Temporal");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Colección Temporal",
                    Descripcion = "Exposiciones temporales que presentan obras de diferentes períodos, estilos y artistas de todo el mundo.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }

        // GET: Colecciones/ColeccionPermanente
        [AllowAnonymous]
        public IActionResult ColeccionPermanente()
        {
            var coleccion = _context.Colecciones
                .Include(c => c.Obras)
                .FirstOrDefault(c => c.Nombre == "Colección Permanente");

            if (coleccion == null)
            {
                coleccion = new Coleccion
                {
                    Nombre = "Colección Permanente",
                    Descripcion = "Nuestra colección permanente incluye obras maestras de todos los tiempos, desde la antigüedad hasta el arte contemporáneo.",
                    Obras = new List<Obra>()
                };
            }
            return View(coleccion);
        }
    }
}
