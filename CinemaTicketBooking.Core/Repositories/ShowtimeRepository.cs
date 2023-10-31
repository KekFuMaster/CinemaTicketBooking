using CinemaTicketBooking.Common.Repositories;
using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Repositories;

public class ShowtimeRepository : Repository<Showtime>, IShowtimeRepository
{
    public ShowtimeRepository(CinemaDbContext context) : base(context)
    {
    }
}