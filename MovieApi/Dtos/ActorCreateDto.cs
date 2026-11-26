using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
	public class ActorCreateDto
	{
		[Required]
		[MaxLength(200)]
		public string Name { get; set; } = string.Empty;
		public int BirthYear { get; set; }
	}
}
