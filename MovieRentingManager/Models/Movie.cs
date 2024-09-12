
namespace MovieRentingManager.Models
{
    public class Movie : BaseObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public int AvailableCopies { get; set; }

    }
}