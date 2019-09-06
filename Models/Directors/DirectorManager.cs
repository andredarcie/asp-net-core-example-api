using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core_example_api.Models.Directors
{
    public class DirectorManager : IDirectorManager
    {
        private readonly ApplicationDbContext _context;

        public DirectorManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Director>> GetAll()
        {
            return await _context.Directors.ToListAsync();
        }

        public async Task<Director> GetById(long id)
        {
            return await _context.Directors.Include(d => d.Movies)
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> Create(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Director director)
        {
            _context.Directors.Update(director);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Remove(Director director)
        {
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}