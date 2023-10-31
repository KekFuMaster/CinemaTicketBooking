using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking.Api.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    
    /// <summary>
    /// Retrieves a reservation by its ID.
    /// </summary>
    /// <param name="id">The ID of the reservation to retrieve.</param>
    /// <returns>The reservation details.</returns>
    /// <response code="200">Returns the reservation details.</response>
    /// <response code="404">If the reservation does not exist.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var reservations = await _reservationService.GetReservationAsync(id);
        return Ok(reservations);
    }
    
    /// <summary>
    /// Retrieves all reservations for a specific showtime.
    /// </summary>
    /// <param name="showtimeId">The ID of the showtime.</param>
    /// <returns>A list of reservations.</returns>
    /// <response code="200">Returns a list of reservations.</response>
    [HttpGet("showtime/{showtimeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByShowtime(int showtimeId)
    {
        var reservations = await _reservationService.GetReservationsByShowtimeAsync(showtimeId);
        return Ok(reservations);
    }
    
    /// <summary>
    /// Creates a new reservation.
    /// </summary>
    /// <param name="showtimeId">The ID of the showtime.</param>
    /// <param name="seatNumbers">The seat numbers to reserve.</param>
    /// <returns>The details of the newly created reservation.</returns>
    /// <response code="201">Returns the newly created reservation details.</response>
    /// <response code="400">If no seat numbers are provided or if any of the seats are already reserved.</response>
    /// <response code="404">If the showtime does not exist.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(int showtimeId, [FromBody] IEnumerable<string> seatNumbers)
    {
        if (seatNumbers == null || !seatNumbers.Any())
            throw new ValidationException("No seat numbers provided.");

        var reservationDto = await _reservationService.ReserveSeatsAsync(showtimeId, seatNumbers);
        return CreatedAtAction(nameof(GetById), new { id = reservationDto.Id}, reservationDto);
    }

    
    /// <summary>
    /// Confirms a reservation.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation to confirm.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the reservation is successfully confirmed.</response>
    /// <response code="400">If the reservation could not be confirmed.</response>
    /// <response code="404">If the reservation does not exist.</response>
    [HttpPatch("{reservationId}/confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Confirm(int reservationId)
    {
        var isConfirmed = await _reservationService.ConfirmReservationAsync(reservationId);
        if (!isConfirmed)
            throw new ValidationException($"Reservation with id {reservationId} could not be confirmed.");

        return NoContent();
    }

    /// <summary>
    /// Cancels a reservation.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation to cancel.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the reservation is successfully cancelled.</response>
    /// <response code="404">If the reservation does not exist or could not be cancelled.</response>
    [HttpDelete("{reservationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(int reservationId)
    {
        var isCancelled = await _reservationService.CancelReservationAsync(reservationId);
        if (!isCancelled)
            return NotFound($"Reservation with id {reservationId} not found or could not be cancelled.");

        return NoContent();
    }
}