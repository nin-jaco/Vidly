using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

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

        public ActionResult Index()
        {
            var movies = Context.Movies.Include(p => p.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = Context.Movies.Include(p => p.Genre).FirstOrDefault(p => p.Id == id);
            if (movie == null) return HttpNotFound();

            return View(movie);
        }
    }
}