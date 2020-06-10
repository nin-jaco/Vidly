using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext Context { get; } = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
        }

        // GET: Movie/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };
            var viewModel = new RandomMovieViewModel { Movie = movie, Customers = customers };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id =" + id);
        }

        //public ActionResult Index()
        //{
        //    var movies = Context.Movies.Include(p => p.Genre).ToList();

        //    return View(movies);
        //}

        public ViewResult Index()
        {
            if (User.IsInRole("CanManageMovies"))
                return View("List");
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
        {
            var genres = Context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = Context.Movies.Include(p => p.Genre).FirstOrDefault(p => p.Id == id);
            if (movie == null) return HttpNotFound();

            return View(movie);
        }

        public ActionResult MovieForm(int id)
        {
            var movie = Context.Movies.FirstOrDefault(p => p.Id == id);
            if (movie == null) return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = Context.Genres.ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                Context.Movies.Add(movie);
            }
            else
            {
                var dbMovie = Context.Movies.FirstOrDefault(p => p.Id == movie.Id);
                //TryUpdateModel(dbCust);
                //automapper
                dbMovie.Name = movie.Name;
                dbMovie.ReleaseDate = movie.ReleaseDate;
                dbMovie.NumberInStock = movie.NumberInStock;
                dbMovie.GenreId = movie.GenreId;
            }

            return RedirectToAction("Index", "Movie");
        }
    }
}