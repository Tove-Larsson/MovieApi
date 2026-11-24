namespace MovieApi.Models
{
	public class Movie
	{
		public int Id {  get; set; }
		public string Title { get; set; }
		public int Year { get; set; }

		public string Genre { get; set; }
		public int Duration { get; set; }
		public MovieDetails? MovieDetails { get; set; }
		public ICollection<Review> Reviews { get; set; } = [];
	}
}
