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
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
    {
        var movies = await _context.Movies
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
				Actors = m.Actors.Select(a => new ActorDto 
                { 
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
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
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int? id, Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int? id)
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

    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
