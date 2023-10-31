using CinemaTicketBooking.Core.Dtos;

namespace CinemaTicketBooking.Core.Services;

public interface IReservationService
{
    Task<ReservationDto> GetReservationAsync(int id);
    Task<IEnumerable<ReservationDto>> GetReservationsByShowtimeAsync(int showtimeId);
    Task<ReservationDto> ReserveSeatsAsync(int showtimeId, IEnumerable<string> seatNumbers);
    Task<bool> ConfirmReservationAsync(int reservationId);
    Task<bool> CancelReservationAsync(int reservationId);
}