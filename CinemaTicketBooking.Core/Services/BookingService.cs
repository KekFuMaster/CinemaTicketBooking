using AutoMapper;
using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Entities;
using CinemaTicketBooking.Core.Repositories;

namespace CinemaTicketBooking.Core.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public BookingService(
        IBookingRepository bookingRepository,
        IReservationRepository reservationRepository,
        IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<BookingDto> ConfirmBookingAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationAsync(reservationId) ??
                          throw new NotFoundException(reservationId, nameof(Reservation));
        if (reservation.IsConfirmed == false)
            throw new ValidationException(
                $"Reservation with id {reservationId} is not confirmed. Confirm it before confirming Booking");
        
        var booking = await _bookingRepository.ConfirmBookingAsync(reservation);
        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDetailsDto> GetBookingDetailsAsync(int bookingId)
    {
        var booking = await _bookingRepository.GetBookingDetailsAsync(bookingId) ??
                      throw new NotFoundException(bookingId, nameof(Booking));

        var bookingDetailsDto = _mapper.Map<BookingDetailsDto>(booking);
        return bookingDetailsDto;
    }
}