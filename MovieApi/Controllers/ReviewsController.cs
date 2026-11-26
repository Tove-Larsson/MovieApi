using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.Dtos;
using System.Linq.Expressions;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly MovieApiContext _context;
    public ReviewsController(MovieApiContext context)
    {
        _context = context;
    }

    // GET: api/Reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReview()
    {
        var reviews = await _context.Reviews
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating,
            })
            .ToListAsync();

        return reviews;
    }

    // GET: api/Reviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetReview(int id)
    {
        var review = await _context.Reviews
            .Where(r => r.Id == id)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating,
            })
            .FirstOrDefaultAsync();

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    // PUT: api/Reviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, ReviewUpdateDto reviewUpdateDto)
    {
        var entity = await _context.Reviews.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.ReviewerName = reviewUpdateDto.ReviewerName;
        entity.Comment = reviewUpdateDto.Comment;
        entity.Rating = reviewUpdateDto.Rating;

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

    // POST: api/Reviews
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> PostReview(ReviewCreateDto reviewCreateDto)
    {
        var entity = new Review
        {
            ReviewerName = reviewCreateDto.ReviewerName,
            Comment = reviewCreateDto.Comment,
            Rating = reviewCreateDto.Rating,
        };

        _context.Reviews.Add(entity);
        await _context.SaveChangesAsync();

        var reviewDto = new ReviewDto
        {
            Id = entity.Id,
            ReviewerName = entity.ReviewerName,
            Comment = entity.Comment,
            Rating = entity.Rating,
        };

        return CreatedAtAction(nameof(GetReview), new { id = reviewDto.Id }, reviewDto);
    }

    // DELETE: api/Reviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET /api/movies/{movieId}/reviews
    [HttpGet("~/api/movies/{movieId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(int movieId)
    {
        var movie = await _context.Movies.FindAsync(movieId);

        if (movie == null)
        {
            return NotFound();
        }

        var reviews = await _context.Reviews
            .Where(r => r.MovieId == movieId)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating
            })
            .ToListAsync();

        return reviews;
    }

    // POST /api/movies/{movieId}/reviews
    [HttpPost("~/api/movies/{movieId}/reviews")]
    public async Task<ActionResult<ReviewDto>> PostReviewToMovie(int movieId, ReviewCreateDto reviewCreateDto)
    {
		var movie = await _context.Movies.FindAsync(movieId);

		if (movie == null)
		{
			return NotFound();
		}

        var entity = new Review
        {
            ReviewerName = reviewCreateDto.ReviewerName,
            Comment = reviewCreateDto.Comment,
            Rating = reviewCreateDto.Rating,
            MovieId = movieId
        };

        _context.Reviews.Add(entity);
        await _context.SaveChangesAsync();

        var reviewDto = new ReviewDto
        {
            Id = entity.Id,
            ReviewerName = entity.ReviewerName,
            Comment = entity.Comment,
            Rating = entity.Rating,
        };

        return CreatedAtAction(nameof(GetReview), new { id = reviewDto.Id }, reviewDto);
	}
}
