using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Data;
using MovieApi.Dtos;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly MovieApiContext _context;
    public ActorsController(MovieApiContext context)
    {
        _context = context;
    }

    // GET: api/Actors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetActor()
    {
		var actor = await _context.Actors
			.Select(a => new ActorDto
			{
				Id = a.Id,
                Name = a.Name,
                BirthYear = a.BirthYear
			})
			.ToListAsync();

        return actor;
    }

    // GET: api/Actors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> GetActor(int id)
    {
        var actor = await _context.Actors
            .Where(a => a.Id == id)
            .Select(a => new ActorDto
            {
                Id = a.Id,
                Name = a.Name,
                BirthYear = a.BirthYear
            })
            .FirstOrDefaultAsync();

        if (actor == null)
        {
            return NotFound();
        }

        return actor;
    }

    // PUT: api/Actors/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActor(int id, ActorUpdateDto actorUpdateDto)
    {
        var entity = await _context.Actors.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.Name = actorUpdateDto.Name;
        entity.BirthYear = actorUpdateDto.BirthYear;

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

    // POST: api/Actors
    [HttpPost]
    public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto createActorDto)
    {
        var entity = new Actor
        {
            Name = createActorDto.Name,
            BirthYear = createActorDto.BirthYear,
        };
        
        _context.Actors.Add(entity);
        await _context.SaveChangesAsync();

        var actorDto = new ActorDto
        {
            Id = entity.Id,
            Name = entity.Name,
            BirthYear = entity.BirthYear
        };

        return CreatedAtAction(nameof(GetActor), new { id = actorDto.Id }, actorDto);
    }

    // DELETE: api/Actors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
        {
            return NotFound();
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
