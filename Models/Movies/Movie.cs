using asp_net_core_example_api.Models.Directors;

namespace asp_net_core_example_api.Models.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public long DirectorId { get; set; }
        public Director Director { get; set; }
    }
}