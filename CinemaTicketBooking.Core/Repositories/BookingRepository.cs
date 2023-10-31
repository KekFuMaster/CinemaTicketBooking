using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking.Core.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly CinemaDbContext _context;

    public BookingRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> ConfirmBookingAsync(Reservation reservation)
    {
        var booking = new Booking
        {
            ReservationId = reservation.Id,
            TotalPrice = CalculateTotalPrice(reservation),
            BookingTime = DateTime.UtcNow
        };

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        return booking;
    }

    public async Task<Booking?> GetBookingAsync(int bookingId)
    {
        return await _context.Bookings.FindAsync(bookingId);
    }

    public async Task<Booking?> GetBookingDetailsAsync(int bookingId)
    {
        return await _context.Bookings
            .Include(b => b.Reservation)
                .ThenInclude(r => r.ReservationSeats)
            .Include(b => b.Reservation)
                .ThenInclude(r => r.Showtime)
                    .ThenInclude(st=> st.Movie)
            .SingleOrDefaultAsync(b => b.Id == bookingId);
    }

    private decimal CalculateTotalPrice(Reservation reservation)
    {
        const decimal pricePerSeat = 10m; // Assume a fixed price per seat for simplicity. Should be as part of logic
        return reservation.ReservationSeats.Count * pricePerSeat;
    }
}