using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking.Api.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    /// <summary>
    /// Retrieves a list of all movies.
    /// </summary>
    /// <returns>A list of movie details.</returns>
    /// <response code="200">Returns the list of movies.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetAllAsync();
        return Ok(movies);
    }

    /// <summary>
    /// Retrieves the details of a specific movie.
    /// </summary>
    /// <param name="id">The ID of the movie to retrieve.</param>
    /// <returns>The details of the specified movie.</returns>
    /// <response code="200">Returns the movie details.</response>
    /// <response code="404">If the movie does not exist.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _movieService.GetAsync(id);
        return Ok(movie);
    }

    /// <summary>
    /// Adds a new movie.
    /// </summary>
    /// <param name="movieDto">The details of the movie to add.</param>
    /// <returns>The details of the newly created movie.</returns>
    /// <response code="201">Returns the newly created movie.</response>
    /// <response code="400">If the movie has an id greater than 0 or is invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddMovie([FromBody] MovieDto movieDto)
    {
        var newMovie = await _movieService.AddAsync(movieDto);
        return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, newMovie);
    }

    /// <summary>
    /// Updates an existing movie.
    /// </summary>
    /// <param name="movieDto">The updated details of the movie.</param>
    /// <returns>The details of the updated movie.</returns>
    /// <response code="200">Returns the updated movie details.</response>
    /// <response code="400">If the movie id is less than or equal to 0 or is invalid.</response>
    /// <response code="404">If the movie does not exist.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMovie([FromBody] MovieDto movieDto)
    {
        var updatedMovie = await _movieService.UpdateAsync(movieDto);
        return Ok(updatedMovie);
    }

    /// <summary>
    /// Deletes a specific movie.
    /// </summary>
    /// <param name="id">The ID of the movie to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the movie is deleted successfully.</response>
    /// <response code="400">If the movie id is less than or equal to 0 or is invalid.</response>
    /// <response code="404">If the movie does not exist.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        await _movieService.DeleteAsync(id);
        return NoContent();
    }
}
