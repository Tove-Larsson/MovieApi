using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
	public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options)
	{
		public DbSet<Movie> Movies { get; set; }

		public DbSet<MovieDetails> MovieDetails { get; set; }

		public DbSet<Review> Reviews { get; set; }

	}
}
