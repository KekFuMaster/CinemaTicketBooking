using CinemaTicketBooking.Common.Entitiea;

namespace CinemaTicketBooking.Core.Entities;

public class ReservationSeat: BaseEntity
{
    public string SeatNumber { get; set; }
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
}