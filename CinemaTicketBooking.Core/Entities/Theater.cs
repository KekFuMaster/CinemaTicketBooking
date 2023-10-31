using CinemaTicketBooking.Common.Entitiea;

namespace CinemaTicketBooking.Core.Entities;

public class Theater : BaseEntity
{
    public string Name { get; set; }
    public int TotalSeats { get; set; }
    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}