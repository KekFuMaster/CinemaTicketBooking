using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.HostedServices;
using CinemaTicketBooking.Core.Profiles;
using CinemaTicketBooking.Core.Repositories;
using CinemaTicketBooking.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaTicketBooking.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCinemaTicketBookingServices(this IServiceCollection services)
    {
        services.AddDbContext<CinemaDbContext>(options =>
            options.UseInMemoryDatabase("CinemaDb"));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
        services.AddScoped<IShowtimeService, ShowtimeService>();
        services.AddScoped<ITheaterRepository, TheaterRepository>();
        services.AddScoped<ITheaterService, TheaterService>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IBookingService, BookingService>();

        services.AddHostedService<ReservationCleanupService>();
        
        return services;
    }
}