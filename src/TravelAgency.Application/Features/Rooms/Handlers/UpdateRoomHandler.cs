using MediatR;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Application.Features.Rooms.Commands;

namespace TravelAgency.Application.Features.Rooms.Handlers;

public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, Unit>
{
    private readonly IRoomRepository _repository;

    public UpdateRoomHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _repository.GetByIdAsync(request.Id);
        room.Taxes = request.Taxes;
        room.Location = request.Location;
        await _repository.UpdateAsync(room);
        return Unit.Value;
    }
}