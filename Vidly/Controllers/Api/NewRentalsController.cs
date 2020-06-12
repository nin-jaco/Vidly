using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.UI.WebControls;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext Context { get; } = new ApplicationDbContext();


        // GET api/NewRentals
        public IHttpActionResult Get()
        {
            var movies = Context.Rentals.ToList().Select(Mapper.Map<Rental, NewRentalDto>);
            return Ok(movies);
        }

        // GET api/NewRentals/5
        public IHttpActionResult Get(int id)
        {
            var movie = Context.Movies.FirstOrDefault(p => p.Id == id);
            if (movie == null) return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/NewRentals
        [HttpPost]
        public IHttpActionResult NewRentals(NewRentalDto rentalDto)
        {
            if (rentalDto.MovieIds.Count == 0) return BadRequest("No Movies were chosen");

            var customer = Context.Customers.FirstOrDefault(p => p.Id == rentalDto.CustomerId);

            if (customer == null) return BadRequest("CustomerId is not valid");

            var movies = Context.Movies.Where(p => rentalDto.MovieIds.Contains(p.Id)).ToList();

            if (movies.Count != rentalDto.MovieIds.Count) return BadRequest("One or more movieIds are invalid.");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0) return BadRequest("Movie is not available");
                
                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                Context.Rentals.Add(rental);
            }

            Context.SaveChanges();

            return Ok();
        }
    }
}
