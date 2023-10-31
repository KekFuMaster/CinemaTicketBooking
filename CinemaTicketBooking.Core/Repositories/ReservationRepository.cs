using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking.Core.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly CinemaDbContext _context;

    public ReservationRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetReservationAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByShowtimeAsync(int showtimeId)
    {
        return await (from r in _context.Reservations
            where r.ShowtimeId == showtimeId
            select r).ToListAsync();
    }

    public async Task<Reservation> ReserveSeatsAsync(int showtimeId, IEnumerable<string> seatNumbers)
    {
        var reservation = new Reservation
        {
            ShowtimeId = showtimeId,
            ReservationTime = DateTime.UtcNow,
            ReservationSeats = seatNumbers.Select(sn => new ReservationSeat { SeatNumber = sn }).ToList()
        };

        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task<bool> ConfirmReservationAsync(Reservation reservation)
    {
        reservation.IsConfirmed = true;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CancelReservationAsync(Reservation reservation)
    {
        _context.Reservations.Remove(reservation);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Reservation>> GetOutdatedReservationsAsync(DateTime cutoffTime)
    {
        return await (from r in _context.Reservations
            where !r.IsConfirmed && r.ReservationTime <= cutoffTime
            select r).ToListAsync();
    }
}