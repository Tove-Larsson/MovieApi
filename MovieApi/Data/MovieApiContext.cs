using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
	public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Movie>().HasData(
				new Movie
				{
					Id = 1,
					Title = "The Fellowship of the Ring",
					Year = 2001,
					Genre = "Fantasy",
					Duration = 178
				},
				new Movie
				{
					Id = 2,
					Title = "The Two Towers",
					Year = 2002,
					Genre = "Fantasy",
					Duration = 179,

				},
				new Movie
				{
					Id = 3,
					Title = "The return of the king",
					Year = 2003,
					Genre = "Fantasy",
					Duration = 201,

				});

			modelBuilder.Entity<MovieDetails>().HasData(
				new MovieDetails
				{
					Id = 1,
					MovieId = 1,
					Synopsis = "A hobbit inherits a powerful ring and sets out with a fellowship to destroy it in the fires of Mount Doom.",
					Language = "English",
					Budget = 93000000
				},
				new MovieDetails
				{
					Id = 2,
					MovieId = 2,
					Synopsis = "The fellowship is split up as Frodo and Sam continue toward Mordor while war comes to Rohan.",
					Language = "English",
					Budget = 94000000
				},
				new MovieDetails
				{
					Id = 3,
					MovieId = 3,
					Synopsis = "The final battle for Middle-earth begins as Frodo and Sam near the end of their journey.",
					Language = "English",
					Budget = 94000000
				});

			modelBuilder.Entity<Review>().HasData(
				new Review { Id = 1, MovieId = 1, ReviewerName = "Anna", Rating = 5, Comment = "A perfect start to the trilogy." },
				new Review { Id = 2, MovieId = 1, ReviewerName = "Johan", Rating = 4, Comment = "Beautiful, but a bit long." },
				new Review { Id = 3, MovieId = 2, ReviewerName = "Sara", Rating = 5, Comment = "Helm's Deep is unforgettable." },
				new Review { Id = 4, MovieId = 2, ReviewerName = "Erik", Rating = 4 },
				new Review { Id = 5, MovieId = 3, ReviewerName = "Lisa", Rating = 5, Comment = "A powerful ending." },
				new Review { Id = 6, MovieId = 3, ReviewerName = "Omar", Rating = 3, Comment = "Great, but too many endings." });

			modelBuilder.Entity<Actor>().HasData(
				new Actor { Id = 1, Name = "Elijah Wood", BirthYear = 1981 },
				new Actor { Id = 2, Name = "Ian McKellen", BirthYear = 1939 },
				new Actor { Id = 3, Name = "Viggo Mortensen", BirthYear = 1958});

			modelBuilder.Entity<MovieActor>().HasData(
				new MovieActor { MovieId = 1, ActorId = 1, Role = "Frodo" },
				new MovieActor { MovieId = 1, ActorId = 2, Role = "Gandalf the grey" },
				new MovieActor { MovieId = 1, ActorId = 3, Role = "Aragorn son of Arathorn" },
				new MovieActor { MovieId = 2, ActorId = 1, Role = "Frodo" },
				new MovieActor { MovieId = 2, ActorId = 2, Role = "Gandalf the white" },
				new MovieActor { MovieId = 2, ActorId = 3, Role = "Aragorn son of Arathorn" },
				new MovieActor { MovieId = 3, ActorId = 1, Role = "Frodo" },
				new MovieActor { MovieId = 3, ActorId = 2, Role = "Gandalf the white" },
				new MovieActor { MovieId = 3, ActorId = 3, Role = "Aragorn son of Arathorn" });




		}

		public DbSet<Movie> Movies { get; set; }

		public DbSet<MovieDetails> MovieDetails { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<Actor> Actors { get; set; }

		public DbSet<MovieActor> MovieActors { get; set; }

	}
}
