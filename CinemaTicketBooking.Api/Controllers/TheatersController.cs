using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking.Api.Controllers;

[ApiController]
[Route("api/theaters")]
public class TheatersController : ControllerBase
{
    private readonly ITheaterService _theaterService;

    public TheatersController(ITheaterService theaterService)
    {
        _theaterService = theaterService;
    }

    /// <summary>
    /// Retrieves a list of all theaters.
    /// </summary>
    /// <returns>A list of theater details.</returns>
    /// <response code="200">Returns the list of theaters.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var theaters = await _theaterService.GetAllAsync();
        return Ok(theaters);
    }

    /// <summary>
    /// Retrieves the details of a specific theater.
    /// </summary>
    /// <param name="id">The ID of the theater to retrieve.</param>
    /// <returns>The details of the specified theater.</returns>
    /// <response code="200">Returns the theater details.</response>
    /// <response code="404">If the theater does not exist.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var theater = await _theaterService.GetAsync(id);
        return Ok(theater);
    }

    /// <summary>
    /// Adds a new theater.
    /// </summary>
    /// <param name="theaterDto">The details of the theater to add.</param>
    /// <returns>The details of the newly created theater.</returns>
    /// <response code="201">Returns the newly created theater.</response>
    /// <response code="400">If the theater has an id greater than 0 or is invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(TheaterDto theaterDto)
    {
        var newTheater = await _theaterService.AddAsync(theaterDto);
        return CreatedAtAction(nameof(Get), new { id = newTheater.Id }, newTheater);
    }

    /// <summary>
    /// Updates an existing theater.
    /// </summary>
    /// <param name="theaterDto">The updated details of the theater.</param>
    /// <returns>The details of the updated theater.</returns>
    /// <response code="200">Returns the updated theater details.</response>
    /// <response code="400">If the theater id is less than or equal to 0 or is invalid.</response>
    /// <response code="404">If the theater does not exist.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(TheaterDto theaterDto)
    {
        var updatedTheater = await _theaterService.UpdateAsync(theaterDto);
        return Ok(updatedTheater);
    }

    /// <summary>
    /// Deletes a specific theater.
    /// </summary>
    /// <param name="id">The ID of the theater to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the theater is deleted successfully.</response>
    /// <response code="400">If the theater id is less than or equal to 0 or is invalid.</response>
    /// <response code="404">If the theater does not exist.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _theaterService.DeleteAsync(id);
        return NoContent();
    }
}