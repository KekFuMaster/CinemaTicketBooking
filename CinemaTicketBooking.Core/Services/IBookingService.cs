using CinemaTicketBooking.Core.Dtos;

namespace CinemaTicketBooking.Core.Services;

public interface IBookingService
{
    Task<BookingDto> ConfirmBookingAsync(int reservationId);
    Task<BookingDetailsDto> GetBookingDetailsAsync(int bookingId);
}