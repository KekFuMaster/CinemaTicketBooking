using AutoMapper;
using CinemaTicketBooking.Common.Services;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Repositories;
using CinemaTicketBooking.Core.Dtos;

namespace CinemaTicketBooking.Core.Services
{
    public class MovieService : BaseService<Movie, MovieDto, IMovieRepository>, IMovieService
    {
        public MovieService(IMovieRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}