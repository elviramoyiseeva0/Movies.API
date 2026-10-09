using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;

namespace Movies.API.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApplicationContext _context;

    public MoviesController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("movies")]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Ok(movies);
    }

    [HttpPost("vote/{movieId}")]
    public async Task<IActionResult> Vote(int movieId)
    {
        var movie = await _context.Movies.FindAsync(movieId);
        if (movie == null)
            return NotFound(new { message = "Movie not found" });

        movie.Votes += 1;
        await _context.SaveChangesAsync();

        return Ok(movie);
    }
}