using System.ComponentModel.DataAnnotations.Schema;

namespace DCSS13663.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public double Rating { get; set; }
        public string Country { get; set; }

        [ForeignKey(nameof(Genre))]
        public int? MovieGenreId { get; set; }
        public Genre? MovieGenre { get; set; }
    }
}
