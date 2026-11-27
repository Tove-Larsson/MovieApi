using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
	public class MovieActorCreateDto
	{
		[Range(1, int.MaxValue, ErrorMessage = "ActorId must be specified and greater than 0")]
		public int ActorId { get; set; }
		public int MovieId { get; set; }
		[Required(ErrorMessage = "Please enter the role the actor plays in the movie")]
		[MaxLength(100, ErrorMessage = "The role can't be longer than 100 characters")]
		public string Role { get; set; }
	}
}
