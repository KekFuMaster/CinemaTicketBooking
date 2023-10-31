using System.ComponentModel.DataAnnotations;
using CinemaTicketBooking.Common.Entitiea;

namespace CinemaTicketBooking.Core.Entities;

public class Reservation: BaseEntity
{
    public int ShowtimeId { get; set; }
    public Showtime Showtime { get; set; }
    public DateTime ReservationTime { get; set; }
    public bool IsConfirmed { get; set; }
    [Required]
    public ICollection<ReservationSeat> ReservationSeats { get; set; } = new List<ReservationSeat>();
}