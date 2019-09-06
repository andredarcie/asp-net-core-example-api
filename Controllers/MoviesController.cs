using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core_example_api.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core_example_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieManager _movieManager;

        public MoviesController(IMovieManager movieManager)
        {
            _movieManager = movieManager;
        }

        // GET api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            try
            {
                var movies = await _movieManager.GetAll();

                if (!movies.Any())
                {
                    return NotFound("Movies not found.");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            try
            {
                var movie = await _movieManager.GetById(id);

                if (movie == null)
                {
                    return NotFound("Movie not found.");
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // POST api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> Post([FromBody] Movie movie)
        {
            try
            {
                await _movieManager.Create(movie);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> Put(int id, [FromBody] Movie movie)
        {
            try
            {
                movie.Id = id;
                await _movieManager.Update(movie);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _movieManager.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}