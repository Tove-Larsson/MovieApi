using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
	public class ActorUpdateDto
	{
		[Required]
		[MaxLength(200)]
		public string Name { get; set; } = string.Empty;
		public int BirthYear { get; set; }
	}
}
