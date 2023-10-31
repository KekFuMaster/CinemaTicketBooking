namespace CinemaTicketBooking.Core.Dtos;

public class BookingDetailsDto
{
    public int BookingId { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public int ShowtimeId { get; set; }
    public DateTime Showtime { get; set; }
    public IEnumerable<string> ReservedSeats { get; set; }
    public decimal TotalPrice { get; set; }
}