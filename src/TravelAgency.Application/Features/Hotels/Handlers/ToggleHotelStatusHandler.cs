
using MediatR;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Handlers;

public class ToggleHotelStatusHandler : IRequestHandler<ToggleHotelStatusCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;

    public ToggleHotelStatusHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Unit> Handle(ToggleHotelStatusCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);
        hotel.ToggleStatus();
        await _hotelRepository.UpdateAsync(hotel);
        return Unit.Value;
    }
}