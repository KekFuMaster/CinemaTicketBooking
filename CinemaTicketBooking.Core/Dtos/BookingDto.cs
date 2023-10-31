using CinemaTicketBooking.Common.Dtos;
using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Dtos;

public class BookingDto : BaseDto
{
    public int ReservationId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime BookingTime { get; set; }
    public IEnumerable<string> SeatNumbers { get; set; }
    public ShowtimeDto Showtime { get; set; }
}