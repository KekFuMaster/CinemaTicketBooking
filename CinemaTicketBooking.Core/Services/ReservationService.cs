using AutoMapper;
using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Repositories;

namespace CinemaTicketBooking.Core.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly IMapper _mapper;

    public ReservationService(
        IReservationRepository reservationRepository,
        IShowtimeRepository showtimeRepository,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _showtimeRepository = showtimeRepository;
        _mapper = mapper;
    }

    public async Task<ReservationDto> GetReservationAsync(int id)
    {
        var reservation = await _reservationRepository.GetReservationAsync(id) ??
                          throw new NotFoundException(id, nameof(Reservation));
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<IEnumerable<ReservationDto>> GetReservationsByShowtimeAsync(int showtimeId)
    {
        var reservations = await _reservationRepository.GetReservationsByShowtimeAsync(showtimeId);
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto> ReserveSeatsAsync(int showtimeId, IEnumerable<string> seatNumbers)
    {
        if (!await _showtimeRepository.DoesExist(showtimeId))
        {
            throw new NotFoundException(showtimeId, nameof(Showtime));
        }

        var existingReservations = await _reservationRepository.GetReservationsByShowtimeAsync(showtimeId);
        var reservedSeatNumbers = existingReservations.SelectMany(r => r.ReservationSeats.Select(s => s.SeatNumber));
        var duplicateSeats = seatNumbers.Where(sn => reservedSeatNumbers.Contains(sn)).ToArray();
        if (duplicateSeats != null && duplicateSeats.Any())
            throw new ValidationException($"These seats are taken: {string.Join(',', duplicateSeats)}");

        var reservation = await _reservationRepository.ReserveSeatsAsync(showtimeId, seatNumbers);
        return _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<bool> ConfirmReservationAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationAsync(reservationId) ??
                          throw new NotFoundException(reservationId, nameof(Reservation));
        return await _reservationRepository.ConfirmReservationAsync(reservation);
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationAsync(reservationId) ??
                          throw new NotFoundException(reservationId, nameof(Reservation));
        return await _reservationRepository.CancelReservationAsync(reservation);
    }
}