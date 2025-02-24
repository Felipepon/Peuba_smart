// En CancelBookingHandler.cs
using MediatR;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Application.Features.Bookings.Commands;


namespace TravelAgency.Application.Features.Bookings.Handlers;

public class CancelBookingHandler : IRequestHandler<CancelBookingCommand, Unit>
{
    private readonly IBookingRepository _repository;

    public CancelBookingHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.BookingId);
        booking.IsCancelled = true;
        await _repository.UpdateAsync(booking);
        return Unit.Value;
    }
}