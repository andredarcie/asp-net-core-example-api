using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core_example_api.Models.Directors;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core_example_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorManager _directorManager;

        public DirectorsController(IDirectorManager directorManager)
        {
            _directorManager = directorManager;
        }

        // GET api/directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> Get()
        {
            try
            {
                var directors = await _directorManager.GetAll();

                if (directors.Count() == 0)
                {
                    return NotFound("No directors found.");
                }

                return Ok(directors);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // GET api/directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> Get(int id)
        {
            try
            {
                var director = await _directorManager.GetById(id);

                if (director == null)
                {
                    return NotFound("Director not found.");
                }

                return Ok(director);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // POST api/directors
        [HttpPost]
        public async Task<ActionResult<Director>> Post([FromBody] Director director)
        {
            try
            {
                await _directorManager.Create(director);

                return Ok(director);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PUT api/directors/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Director>> Put(long id, [FromBody] Director director)
        {
            try
            {
                var directorInDb = await _directorManager.GetById(id);

                if (directorInDb == null)
                {
                    return NotFound("Director not found.");
                }

                await _directorManager.Update(director);

                return Ok(director);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE api/directors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var directorInDb = await _directorManager.GetById(id);

                if (directorInDb == null)
                {
                    return NotFound("Director not found.");
                }

                await _directorManager.Remove(directorInDb);

                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}