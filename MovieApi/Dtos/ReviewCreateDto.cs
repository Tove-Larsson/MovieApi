using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
	public class ReviewCreateDto
	{
		[Required]
		[MaxLength(200)]
		[MinLength(3)]
		public string ReviewerName { get; set; }
		public string? Comment { get; set; }
		[Range(1, 5)]
		public int Rating { get; set; }
	}
}
