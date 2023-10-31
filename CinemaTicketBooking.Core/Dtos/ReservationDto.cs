using CinemaTicketBooking.Common.Dtos;

namespace CinemaTicketBooking.Core.Dtos;

public class ReservationDto : BaseDto
{
    public int ShowtimeId { get; set; }
    public DateTime ReservationTime { get; set; }
    public bool IsConfirmed { get; set; }
    public IEnumerable<string> SeatNumbers { get; set; }
}