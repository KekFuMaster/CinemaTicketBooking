using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking.Api.Controllers;

[ApiController]
[Route("api/showtimes")]
public class ShowtimesController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;

    public ShowtimesController(IShowtimeService showtimeService)
    {
        _showtimeService = showtimeService;
    }

    /// <summary>
    /// Retrieves a list of all showtimes.
    /// </summary>
    /// <returns>A list of showtime details.</returns>
    /// <response code="200">Returns the list of showtimes.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var showtimes = await _showtimeService.GetAllAsync();
        return Ok(showtimes);
    }

    /// <summary>
    /// Retrieves the details of a specific showtime.
    /// </summary>
    /// <param name="id">The ID of the showtime to retrieve.</param>
    /// <returns>The details of the specified showtime.</returns>
    /// <response code="200">Returns the showtime details.</response>
    /// <response code="404">If the showtime does not exist.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var showtime = await _showtimeService.GetAsync(id);
        return Ok(showtime);
    }

    /// <summary>
    /// Adds a new showtime.
    /// </summary>
    /// <param name="showtimeDto">The details of the showtime to add.</param>
    /// <returns>The details of the newly created showtime.</returns>
    /// <response code="201">Returns the newly created showtime.</response>
    /// <response code="400">If the showtime has an id greater than 0 or is invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(ShowtimeDto showtimeDto)
    {
        var newShowtime = await _showtimeService.AddAsync(showtimeDto);
        return CreatedAtAction(nameof(Get), new { id = newShowtime.Id }, newShowtime);
    }

    /// <summary>
    /// Updates an existing showtime.
    /// </summary>
    /// <param name="showtimeDto">The updated details of the showtime.</param>
    /// <returns>The details of the updated showtime.</returns>
    /// <response code="200">Returns the updated showtime details.</response>
    /// <response code="400">If the showtime id is less than or equal to 0 or is invalid.</response>
    /// <response code="404">If the showtime does not exist.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(ShowtimeDto showtimeDto)
    {
        var updatedShowtime = await _showtimeService.UpdateAsync(showtimeDto);
        return Ok(updatedShowtime);
    }

    /// <summary>
    /// Deletes a specific showtime.
    /// </summary>
    /// <param name="id">The ID of the showtime to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the showtime is deleted successfully.</response>
    /// <response code="400">If the showtime id is less than or equal to 0 or is invalid.</response>
    /// <response code="404">If the showtime does not exist.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _showtimeService.DeleteAsync(id);
        return NoContent();
    }
}