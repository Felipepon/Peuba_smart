using MediatR;

namespace TravelAgency.Application.Features.Rooms.Commands;

public record ToggleRoomStatusCommand(
    Guid Id
) : IRequest<Unit>;