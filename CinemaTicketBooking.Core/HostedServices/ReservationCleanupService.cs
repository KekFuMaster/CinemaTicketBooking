using CinemaTicketBooking.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CinemaTicketBooking.Core.HostedServices;

public class ReservationCleanupService : BackgroundService
{
    private readonly ILogger<ReservationCleanupService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private const int OutdatedReservationSeconds = 60; // Set timeout duration as needed. Should be in configs
    private const int WorkerDelaySeconds = 10;

    public ReservationCleanupService(
        ILogger<ReservationCleanupService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(ReservationCleanupService)} started.");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"{nameof(ReservationCleanupService)} is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var reservationRepository = scope.ServiceProvider
                    .GetRequiredService<IReservationRepository>();

                var outdatedReservations = await reservationRepository
                    .GetOutdatedReservationsAsync(DateTime.UtcNow.AddSeconds(-OutdatedReservationSeconds));

                if (outdatedReservations != null && outdatedReservations.Any())
                {
                    foreach (var reservation in outdatedReservations)
                    {
                        await reservationRepository.CancelReservationAsync(reservation);
                    }
                }
            }

            _logger.LogInformation($"{nameof(ReservationCleanupService)} finished working.");
            await Task.Delay(TimeSpan.FromSeconds(WorkerDelaySeconds), stoppingToken);
        }

        _logger.LogInformation($"{nameof(ReservationCleanupService)} ended.");
    }
}