
using MediatR;
using TravelAgency.Application.Features.Rooms.Queries;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Rooms.Handlers;

public class GetRoomHandler : IRequestHandler<GetRoomQuery, Room>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.GetByIdAsync(request.RoomId);
    }
}