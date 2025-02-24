using MediatR;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Application.Features.Rooms.Commands;


namespace TravelAgency.Application.Features.Rooms.Handlers;

public class ToggleRoomStatusHandler : IRequestHandler<ToggleRoomStatusCommand, Unit>
{
    private readonly IRoomRepository _repository;

    public ToggleRoomStatusHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(ToggleRoomStatusCommand request, CancellationToken cancellationToken)
    {
        var room = await _repository.GetByIdAsync(request.Id);
        room.IsEnabled = !room.IsEnabled;
        await _repository.UpdateAsync(room);
        return Unit.Value;
    }
}