using AutoMapper;
using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Profiles;
using CinemaTicketBooking.Core.Repositories;
using CinemaTicketBooking.Core.Services;
using Moq;
using Xunit;

namespace CinemaTicketBooking.Tests;

public class ReservationServiceTests
{
    private readonly Mock<IReservationRepository> _mockReservationRepository;
    private readonly Mock<IShowtimeRepository> _mockShowtimeRepository;
    private readonly IMapper _mapper;
    private readonly ReservationService _service;

    public ReservationServiceTests()
    {
        _mockReservationRepository = new Mock<IReservationRepository>();
        _mockShowtimeRepository = new Mock<IShowtimeRepository>();
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

        _mapper = mappingConfig.CreateMapper();
        _service = new ReservationService(_mockReservationRepository.Object, _mockShowtimeRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetReservationAsync_ShouldReturnReservationDto()
    {
        // Arrange
        var reservation = new Reservation { Id = 1 };
        var reservationDto = new ReservationDto { Id = 1 };
        _mockReservationRepository.Setup(repo => repo.GetReservationAsync(1)).ReturnsAsync(reservation);

        // Act
        var result = await _service.GetReservationAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reservationDto.Id, result.Id);
    }

    [Fact]
    public async Task ReserveSeatsAsync_ShouldReserveSeats()
    {
        // Arrange
        var seatNumbers = new List<string> { "A1", "A2" };
        var showtimeId = 1;
        var reservation = new Reservation { Id = 1 };
        var reservationDto = new ReservationDto { Id = 1 };
        _mockShowtimeRepository.Setup(repo => repo.DoesExist(showtimeId)).ReturnsAsync(true);
        _mockReservationRepository.Setup(repo => repo.ReserveSeatsAsync(showtimeId, seatNumbers))
            .ReturnsAsync(reservation);

        // Act
        var result = await _service.ReserveSeatsAsync(showtimeId, seatNumbers);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reservationDto.Id, result.Id);
    }

    [Fact]
    public async Task ConfirmReservationAsync_ShouldConfirmReservation()
    {
        // Arrange
        var reservationId = 1;
        var reservation = new Reservation { Id = reservationId };

        _mockReservationRepository.Setup(repo => repo.GetReservationAsync(reservationId)).ReturnsAsync(reservation);
        _mockReservationRepository.Setup(repo => repo.ConfirmReservationAsync(reservation)).ReturnsAsync(true);

        // Act
        var result = await _service.ConfirmReservationAsync(reservationId);

        // Assert
        Assert.True(result);
        _mockReservationRepository.Verify(repo => repo.ConfirmReservationAsync(reservation), Times.Once());
    }

    [Fact]
    public async Task ReserveSeatsAsync_ShouldThrowNotFoundException_ForNonExistentShowtime()
    {
        // Arrange
        var showtimeId = 1;
        var seatNumbers = new List<string> { "A1", "A2" };
        _mockShowtimeRepository.Setup(repo => repo.DoesExist(showtimeId)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.ReserveSeatsAsync(showtimeId, seatNumbers));
    }

    [Fact]
    public async Task ReserveSeatsAsync_ShouldThrowValidationException_ForAlreadyReservedSeats()
    {
        // Arrange
        var showtimeId = 1;
        var seatNumbers = new List<string> { "A1", "A2" };
        var existingReservations = new List<Reservation>
        {
            new Reservation
            {
                ReservationSeats = new List<ReservationSeat>
                {
                    new ReservationSeat { SeatNumber = "A1" }
                }
            }
        };
        _mockShowtimeRepository.Setup(repo => repo.DoesExist(showtimeId)).ReturnsAsync(true);
        _mockReservationRepository.Setup(repo => repo.GetReservationsByShowtimeAsync(showtimeId))
            .ReturnsAsync(existingReservations);

        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<ValidationException>(() => _service.ReserveSeatsAsync(showtimeId, seatNumbers));
        Assert.Contains("These seats are taken: A1", exception.Message);
    }
}