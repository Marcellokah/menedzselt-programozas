using Microsoft.AspNetCore.Mvc;
using ZeneApp.Common;

namespace ZeneApp.Web.Controllers
{
    public class ZeneController : Controller
    {
        private readonly ZeneContext _context;

        public ZeneController(ZeneContext context)
        {
            _context = context;
        }

        // GET: Zene (Böngészés)
        public IActionResult Index()
        {
            // Prioritás szerint rendezve
            var zenek = _context.Zenek.OrderByDescending(z => z.Prioritas).ToList();
            return View(zenek);
        }

        // GET: Zene/Create (Új zene űrlap)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zene/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Zene zene)
        {
            if (ModelState.IsValid)
            {
                // Egyediség ellenőrzése
                if (_context.Zenek.Any(z => z.Cim == zene.Cim))
                {
                    ModelState.AddModelError("Cim", "Ez a cím már létezik!");
                    return View(zene);
                }

                _context.Add(zene);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(zene);
        }
    }
}