using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Dtos;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieApiContext _context;
    public MoviesController(MovieApiContext context)
    {
        _context = context;
    }

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie(string? genre, int? year)
    {
		IQueryable<Movie> query = _context.Movies;

		if (genre != null)
		{
			query = query.Where(m => m.Genre.Contains(genre));
		}

		if (year.HasValue)
		{
			query = query.Where(m => m.Year == year);
		}

		var movies = await query
			.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration
            })
            .ToListAsync();

        return movies;
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration
            })
            .FirstOrDefaultAsync();

		if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

	// GET: api/Movies/5/details
	[HttpGet("{id}/details")]
	public async Task<ActionResult<MovieDetailDto>> GetDetails(int id)
    {
        var movieDetails = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDetailDto
            {
                Synopsis = m.MovieDetails.Synopsis,
                Language = m.MovieDetails.Language,
                Budget = m.MovieDetails.Budget,
				Movie = new MovieDto
				{
					Id = m.Id,
					Title = m.Title,
					Year = m.Year,
					Genre = m.Genre,
					Duration = m.Duration
				},
				Actors = m.MovieActors.Select(ma => new ActorDto 
                { 
                    Id = ma.Actor.Id,
                    Name = ma.Actor.Name,
                    BirthYear = ma.Actor.BirthYear,

                }).ToList(),
                Reviews = m.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList()
			})
            .FirstOrDefaultAsync();

        if (movieDetails == null)
        {  
            return NotFound(); 
        }

        return movieDetails;
    }

    // PUT: api/Movies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto updateMovieDto)
    {
        var entity = await _context.Movies.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.Title = updateMovieDto.Title;
        entity.Year = updateMovieDto.Year;
        entity.Genre = updateMovieDto.Genre;
        entity.Duration = updateMovieDto.Duration;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict();
        }

        return NoContent();
    }

    // POST: api/Movies
    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movieCreateDto)
    {
        // Creates a new movie entity and copy the values from the incoming DTO
        var entity = new Movie
        {
            Title = movieCreateDto.Title,
            Year = movieCreateDto.Year,
            Genre = movieCreateDto.Genre,
            Duration = movieCreateDto.Duration
        };

        _context.Movies.Add(entity);
        await _context.SaveChangesAsync();


		// Creates a MovieDto to send back to the client.
		var movieDto = new MovieDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Year = entity.Year,
            Genre = entity.Genre,
            Duration = entity.Duration
        };

        return CreatedAtAction(nameof(GetMovie), new { id = movieDto.Id }, movieDto);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
