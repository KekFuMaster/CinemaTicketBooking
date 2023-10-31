using CinemaTicketBooking.Common.Entitiea;

namespace CinemaTicketBooking.Core.Entities;

public class Showtime : BaseEntity
{
    public DateTime ShowDateTime { get; set; }
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public Theater Theater { get; set; }
    public int TheaterId { get; set; }
}