using CinemaTicketBooking.Common.Dtos;

namespace CinemaTicketBooking.Core.Entities;

public class ShowtimeDto : BaseDto
{
    public DateTime ShowDateTime { get; set; }
    public int MovieId { get; set; }
    public int TheaterId { get; set; }
}