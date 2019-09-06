using asp_net_core_example_api.Models.Directors;
using asp_net_core_example_api.Models.Movies;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core_example_api.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}