using Microsoft.EntityFrameworkCore;

namespace MovieApi.Data
{
	public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options)
	{
		public DbSet<MovieApi.Models.Movie> Movies { get; set; }

		
	}
}
