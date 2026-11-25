namespace MovieApi.Dtos
{
	public class ReviewCreateDto
	{
		public string ReviewerName { get; set; }
		public string? Comment { get; set; }
		public int Rating { get; set; }
	}
}
