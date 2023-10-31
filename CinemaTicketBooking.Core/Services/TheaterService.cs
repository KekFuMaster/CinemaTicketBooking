using AutoMapper;
using CinemaTicketBooking.Common.Services;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Repositories;
using CinemaTicketBooking.Core.Dtos;

namespace CinemaTicketBooking.Core.Services
{
    public class TheaterService : BaseService<Theater, TheaterDto, ITheaterRepository>, ITheaterService
    {
        public TheaterService(ITheaterRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}