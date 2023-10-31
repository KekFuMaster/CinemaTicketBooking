using CinemaTicketBooking.Common.Repositories;
using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Repositories;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(CinemaDbContext context) : base(context)
    {
    }
}