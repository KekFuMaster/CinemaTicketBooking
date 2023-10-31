using CinemaTicketBooking.Common.Dtos;

namespace CinemaTicketBooking.Core.Entities;

public class TheaterDto : BaseDto
{
    public string Name { get; set; }
    public int TotalSeats { get; set; }
}