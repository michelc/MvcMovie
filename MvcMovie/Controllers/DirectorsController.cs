using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Helpers;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly MvcMovieContext _context;

        public DirectorsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Directors
        public async Task<IActionResult> Index()
        {
            var model = await (from x in _context.Directors
                               orderby x.Name
                               select new DirectorViewModel
                               {
                                   Director_ID = x.Director_ID,
                                   Name = x.Name
                               }).ToListAsync();

            return View(model);
        }

        // GET: Directors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await GetDirectorViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model.ToDirector());
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Directors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await GetDirectorViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DirectorViewModel model)
        {
            if (id != model.Director_ID)
            {
                ViewBag.AlertDanger = "L'identifiant n'est plus valide. Vous devez annuler la modification.";
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.ToDirector());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(model.Director_ID))
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

            return View(model);
        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await GetDirectorViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var model = await _context.Directors.FindAsync(id);
                _context.Directors.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ViewBag.AlertDanger = ex.GetMessage();
            }

            return View(await GetDirectorViewModel(id));

        }

        private async Task<DirectorViewModel> GetDirectorViewModel(int? id)
        {
            if (id == null) return null;

            var model = await (from x in _context.Directors
                               where x.Director_ID == id
                               orderby x.Name
                               select new DirectorViewModel
                               {
                                   Director_ID = x.Director_ID,
                                   Name = x.Name
                               }).FirstOrDefaultAsync();

            return model;
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.Director_ID == id);
        }
    }
}
