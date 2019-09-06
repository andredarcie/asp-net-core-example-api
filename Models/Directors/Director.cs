using System.Collections.Generic;
using asp_net_core_example_api.Models.Movies;

namespace asp_net_core_example_api.Models.Directors
{
    public class Director
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }

        public Director()
        {
            Movies = new List<Movie>();
        }
    }
}