using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext Context { get; } = new ApplicationDbContext();

        // GET api/Movies
        public IHttpActionResult Get(string query = null)
        {
            var moviesQuery = Context.Movies.Include("Genre").Where(p => p.NumberAvailable >0);
            if(!string.IsNullOrWhiteSpace(query)) moviesQuery = moviesQuery.Where(p => p.Name.Contains(query));
                
            return Ok(moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        // GET api/Movies/5
        public IHttpActionResult Get(int id)
        {
            var movie = Context.Movies.FirstOrDefault(p => p.Id == id);
            if (movie == null) return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/Movies
        [HttpPost]
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult Post(MovieDto movieDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            Context.Movies.Add(movie);
            Context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT api/Movies/5
        [HttpPut]
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult Put(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var movieDb = Context.Movies.FirstOrDefault(p => p.Id == id);
            if (movieDb == null) return NotFound();

            Mapper.Map(movieDto, movieDb);
            Context.SaveChanges();
            //return Movie;
            return Ok(movieDto);
        }

        // DELETE api/Movies/5
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult Delete(int id)
        {
            var movieDb = Context.Movies.FirstOrDefault(p => p.Id == id);
            if (movieDb == null) return NotFound();
            Context.Movies.Remove(movieDb);
            Context.SaveChanges();
            return Ok(Mapper.Map<Movie, MovieDto>(movieDb));
        }
    }
}
