using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking.Api.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Confirms a booking based on a reservation.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation to confirm.</param>
    /// <returns>The details of the newly created booking.</returns>
    /// <response code="201">Returns the newly created booking details.</response>
    /// <response code="400">If the reservation is not confirmed or is invalid.</response>
    /// <response code="404">If the reservation does not exist.</response>
    [HttpPost("confirm/{reservationId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmBooking(int reservationId)
    {
        var bookingDto = await _bookingService.ConfirmBookingAsync(reservationId) ??
                         throw new ValidationException($"Reservation with id {reservationId} could not be confirmed.");
        return CreatedAtAction(nameof(GetBooking), new { bookingId = bookingDto.Id }, bookingDto);
    }

    
    /// <summary>
    /// Retrieves the details of a specific booking.
    /// </summary>
    /// <param name="bookingId">The ID of the booking to retrieve.</param>
    /// <returns>The details of the specified booking.</returns>
    /// <response code="200">Returns the booking details.</response>
    /// <response code="404">If the booking does not exist.</response>
    [HttpGet("{bookingId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBooking(int bookingId)
    {
        var bookingDetailsDto = await _bookingService.GetBookingDetailsAsync(bookingId);
        return Ok(bookingDetailsDto);
    }
}