using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Helpers;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class GenresController : Controller
    {
        private readonly MvcMovieContext _context;

        public GenresController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            var model = await (from x in _context.Genres
                               orderby x.Title
                               select new GenreViewModel
                               {
                                   Genre_ID = x.Genre_ID,
                                   Title = x.Title
                               }).ToListAsync();

            return View(model);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await GetGenreViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model.ToGenre());
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await GetGenreViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenreViewModel model)
        {
            if (id != model.Genre_ID)
            {
                ViewBag.AlertDanger = "L'identifiant n'est plus valide. Vous devez annuler la modification.";
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.ToGenre());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(model.Genre_ID))
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

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await GetGenreViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var model = await _context.Genres.FindAsync(id);
                _context.Genres.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ViewBag.AlertDanger = ex.GetMessage();
            }

            return View(await GetGenreViewModel(id));

        }

        private async Task<GenreViewModel> GetGenreViewModel(int? id)
        {
            if (id == null) return null;

            var model = await (from x in _context.Genres
                               where x.Genre_ID == id
                               orderby x.Title
                               select new GenreViewModel
                               {
                                   Genre_ID = x.Genre_ID,
                                   Title = x.Title
                               }).FirstOrDefaultAsync();

            return model;
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Genre_ID == id);
        }
    }
}
