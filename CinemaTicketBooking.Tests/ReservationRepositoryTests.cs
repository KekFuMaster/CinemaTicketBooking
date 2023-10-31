using CinemaTicketBooking.Core.Data;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace CinemaTicketBooking.Tests;

public class ReservationRepositoryTests
{
    private readonly CinemaDbContext _context;
    private readonly ReservationRepository _repository;

    public ReservationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<CinemaDbContext>()
            .UseInMemoryDatabase("TestDatabase", new InMemoryDatabaseRoot())
            .Options;

        _context = new CinemaDbContext(options);
        _repository = new ReservationRepository(_context);
    }

    [Fact]
    public async Task GetReservationAsync_ShouldReturnReservation()
    {
        // Arrange
        var reservation = new Reservation { Id = 1 };
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetReservationAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task ReserveSeatsAsync_ShouldReserveSeats()
    {
        // Arrange
        var seatNumbers = new List<string> { "A1", "A2" };

        // Act
        var result = await _repository.ReserveSeatsAsync(1, seatNumbers);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.ShowtimeId);
        Assert.Equal(2, result.ReservationSeats.Count);
    }

    [Fact]
    public async Task ConfirmReservationAsync_ShouldConfirmReservation()
    {
        // Arrange
        var reservation = new Reservation { Id = 1 };
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.ConfirmReservationAsync(reservation);

        // Assert
        Assert.True(result);
        var confirmedReservation = await _repository.GetReservationAsync(1);
        Assert.True(confirmedReservation.IsConfirmed);
    }
}