using CinemaTicketBooking.Common.Repositories;
using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Repositories;

public class TheaterRepository: Repository<Theater>, ITheaterRepository
{
    public TheaterRepository(CinemaDbContext context) : base(context)
    {
    }
}