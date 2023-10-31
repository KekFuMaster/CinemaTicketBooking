using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetReservationAsync(int id);
    Task<IEnumerable<Reservation>> GetReservationsByShowtimeAsync(int showtimeId);
    Task<Reservation> ReserveSeatsAsync(int showtimeId, IEnumerable<string> seatNumbers);
    Task<bool> ConfirmReservationAsync(Reservation reservation);
    Task<bool> CancelReservationAsync(Reservation reservation);
    Task<IEnumerable<Reservation>> GetOutdatedReservationsAsync(DateTime cutoffTime);
}