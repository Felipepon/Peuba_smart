using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using MediatR;
using TravelAgency.Application.Features.Rooms.Commands;
using TravelAgency.Application.Features.Bookings.Queries;

namespace TravelAgency.Application.Features.Bookings.Handlers;

public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, Booking>
{
    private readonly IBookingRepository _repository;

    public GetBookingByIdHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Booking> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.BookingId, includes: b => b.Guests);
        booking.EmergencyContact = await _repository.GetEmergencyContactAsync(request.BookingId);
        return booking;
    }
}