using AutoMapper;
using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Common.Services;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Repositories;

namespace CinemaTicketBooking.Core.Services
{
    public class ShowtimeService : BaseService<Showtime, ShowtimeDto, IShowtimeRepository>, IShowtimeService
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly IMovieRepository _movieRepository;

        public ShowtimeService(IShowtimeRepository repository, ITheaterRepository theaterRepository,
            IMovieRepository movieRepository, IMapper mapper) : base(
            repository, mapper)
        {
            _theaterRepository = theaterRepository;
            _movieRepository = movieRepository;
        }

        protected override async Task OnSaving(Showtime entity)
        {
            if (!await _theaterRepository.DoesExist(entity.TheaterId))
            {
                throw new NotFoundException(entity.TheaterId, nameof(Theater));
            }

            if (!await _movieRepository.DoesExist(entity.MovieId))
            {
                throw new NotFoundException(entity.MovieId, nameof(Movie));
            }
        }
    }
}