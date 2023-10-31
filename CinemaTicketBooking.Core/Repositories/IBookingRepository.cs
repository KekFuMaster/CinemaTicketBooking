using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Repositories;

public interface IBookingRepository
{
    Task<Booking> ConfirmBookingAsync(Reservation reservation);
    Task<Booking?> GetBookingAsync(int bookingId);
    
    Task<Booking?> GetBookingDetailsAsync(int bookingId);
}