using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
	public class MovieActorCreateDto
	{
		[Range(1, int.MaxValue, ErrorMessage = "ActorId must be specified and greater than 0")]
		public int ActorId { get; set; }
		public int MovieId { get; set; }
		public string Role { get; set; }
	}
}
