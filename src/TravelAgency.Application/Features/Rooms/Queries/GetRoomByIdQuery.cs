
using MediatR;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Features.Rooms.Queries;

public record GetRoomByIdQuery(Guid RoomId) : IRequest<Room>;