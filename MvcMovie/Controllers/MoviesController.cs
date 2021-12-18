using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Helpers;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var model = await (from x in _context.Movies
                               orderby x.Title
                               select new MovieViewModel
                               {
                                   Movie_ID = x.Movie_ID,
                                   Title = x.Title,
                                   Year = x.ReleaseDate.Year,
                                   Genre = x.Genre.Title,
                                   Price = x.Price
                               }).ToListAsync();

            return View(model);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await GetMovieDisplayViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var model = new MovieEditorViewModel();

            model.Genres = await GetMovieEditorGenres();
            return View(model);
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieEditorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model.ToMovie());
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            model.Genres = await GetMovieEditorGenres();
            return View(model);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await GetMovieEditorViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Genres = await GetMovieEditorGenres();
            return View(model);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieEditorViewModel model)
        {
            if (id != model.Movie_ID)
            {
                ViewBag.AlertDanger = "L'identifiant n'est plus valide. Vous devez annuler la modification.";
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.ToMovie());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(model.Movie_ID))
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

            model.Genres = await GetMovieEditorGenres();
            return View(model);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await GetMovieDisplayViewModel(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var model = await _context.Movies.FindAsync(id);
                _context.Movies.Remove(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ViewBag.AlertDanger = ex.GetMessage();
            }

            return View(await GetMovieDisplayViewModel(id));
        }

        private async Task<MovieDisplayViewModel> GetMovieDisplayViewModel(int? id)
        {
            if (id == null) return null;

            var model = await (from x in _context.Movies
                               where x.Movie_ID == id
                               select new MovieDisplayViewModel
                               {
                                   Movie_ID = x.Movie_ID,
                                   Title = x.Title,
                                   ReleaseDate = x.ReleaseDate,
                                   Genre = x.Genre.Title,
                                   Price = x.Price,
                                   Rating = x.Rating
                               }).FirstOrDefaultAsync();

            return model;
        }

        private async Task<MovieEditorViewModel> GetMovieEditorViewModel(int? id)
        {
            if (id == null) return null;

            var model = await (from x in _context.Movies
                               where x.Movie_ID == id
                               select new MovieEditorViewModel
                               {
                                   Movie_ID = x.Movie_ID,
                                   Title = x.Title,
                                   ReleaseDate = x.ReleaseDate,
                                   Genre_ID = x.Genre_ID,
                                   Price = x.Price,
                                   Rating = x.Rating
                               }).FirstOrDefaultAsync();

            return model;
        }

        private async Task<List<SelectListItem>> GetMovieEditorGenres()
        {
            return await (from x in _context.Genres
                          orderby x.Title
                          select new SelectListItem
                          {
                              Value = x.Genre_ID.ToString(),
                              Text = x.Title
                          }).ToListAsync();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Movie_ID == id);
        }
    }
}
