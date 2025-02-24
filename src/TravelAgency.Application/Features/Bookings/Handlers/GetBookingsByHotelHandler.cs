using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using MediatR;
using TravelAgency.Application.Features.Rooms.Commands;
using TravelAgency.Application.Features.Bookings.Queries;

namespace TravelAgency.Application.Features.Bookings.Handlers;

public class GetBookingsByHotelHandler : IRequestHandler<GetBookingsByHotelQuery, List<Booking>>
{
    private readonly IBookingRepository _repository;

    public GetBookingsByHotelHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Booking>> Handle(GetBookingsByHotelQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetBookingsByHotelIdAsync(request.HotelId);
    }
}