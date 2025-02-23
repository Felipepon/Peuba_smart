using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Rooms.Handlers;

public class GetRoomsByHotelHandler : IRequestHandler<GetRoomsByHotelQuery, List<Room>>
{
    private readonly IRoomRepository _repository;

    public GetRoomsByHotelHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Room>> Handle(GetRoomsByHotelQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRoomsByHotelIdAsync(request.HotelId);
    }
}