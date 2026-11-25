namespace MovieApi.Dtos
{
	public class MovieDetailDto
	{
		public string Synopsis { get; set; }
		public string Language { get; set; }
		public int? Budget { get; set; }
		public MovieDto Movie { get; set; }
		public List<ActorDto> Actors { get; set; } = [];
		public List<ReviewDto> Reviews { get; set; } = [];
	}
}
