using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core_example_api.Models.Movies
{
    public class MovieManager : IMovieManager
    {
        private readonly ApplicationDbContext _context;

        public MovieManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(long id)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> Create(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Remove(long id)
        {
            var movie = await this.GetById(id);

            if (movie != null)
            {
                _context.Remove(movie);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}