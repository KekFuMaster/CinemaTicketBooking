using CinemaTicketBooking.Common.Entitiea;

namespace CinemaTicketBooking.Core.Entities;

public class Booking : BaseEntity
{
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime BookingTime { get; set; }
}