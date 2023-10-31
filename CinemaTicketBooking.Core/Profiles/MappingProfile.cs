using AutoMapper;
using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Entities;

namespace CinemaTicketBooking.Core.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MovieDto, Movie>().ReverseMap();
        CreateMap<ShowtimeDto, Showtime>().ReverseMap();
        CreateMap<TheaterDto, Theater>().ReverseMap();
        CreateMap<ReservationDto, Reservation>().ReverseMap();
        CreateMap<BookingDto, Booking>().ReverseMap();
        
        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.SeatNumbers,
                opt => opt.MapFrom(src => src.ReservationSeats.Select(s => s.SeatNumber)));

        CreateMap<ReservationDto, Reservation>()
            .ForMember(dest => dest.ReservationSeats,
                opt => opt.MapFrom(src => src.SeatNumbers.Select(sn => new ReservationSeat { SeatNumber = sn })));
        
        CreateMap<Booking, BookingDetailsDto>()
            .ForMember(
                dest => dest.BookingId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.MovieId,
                opt => opt.MapFrom(src => src.Reservation.Showtime.Movie.Id))
            .ForMember(
                dest => dest.MovieTitle,
                opt => opt.MapFrom(src => src.Reservation.Showtime.Movie.Title))
            .ForMember(
                dest => dest.ShowtimeId,
                opt => opt.MapFrom(src => src.Reservation.Showtime.Id))
            .ForMember(
                dest => dest.Showtime,
                opt => opt.MapFrom(src => src.Reservation.Showtime.ShowDateTime))
            .ForMember(
                dest => dest.ReservedSeats,
                opt => opt.MapFrom(src => src.Reservation.ReservationSeats.Select(s => s.SeatNumber)))
            .ForMember(
                dest => dest.TotalPrice,
                opt => opt.MapFrom(src => src.TotalPrice));

        
    }
}
