using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Rooms.Handlers;

public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdQuery, Room>
{
    private readonly IRoomRepository _repository;

    public GetRoomByIdHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<Room> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}