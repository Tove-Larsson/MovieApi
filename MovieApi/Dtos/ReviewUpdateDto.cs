namespace MovieApi.Dtos
{
	public class ReviewUpdateDto
	{
		public string ReviewerName { get; set; }
		public string? Comment { get; set; }
		public int Rating { get; set; }
	}
}
