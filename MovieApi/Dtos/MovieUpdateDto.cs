
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
	public class MovieUpdateDto
	{
		[Required(ErrorMessage = "Please enter the Title of the movie")]
		[MinLength(1)]
		[MaxLength(150)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Please enter the year the movie was released, between 1888 and 2026")]
		[Range(1888, 2026)]
		public int Year { get; set; }

		[Required(ErrorMessage = "Please enter a genre with maximum 50 characters")]
		[MaxLength(50)]
		public string Genre { get; set; }

		[Range(1, 1000)]
		public int Duration { get; set; }
	}
}
