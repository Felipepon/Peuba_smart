using MediatR;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Commands;

public class UpdateHotelHandler : IRequestHandler<UpdateHotelCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;

    public UpdateHotelHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Unit> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);
        hotel.Name = request.Name;
        hotel.City = request.City;
        await _hotelRepository.UpdateAsync(hotel);
        return Unit.Value;
    }
}