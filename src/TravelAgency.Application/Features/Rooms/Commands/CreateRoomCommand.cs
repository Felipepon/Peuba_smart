using MediatR;
using TravelAgency.Domain.Enums;

namespace TravelAgency.Application.Features.Rooms.Commands;

public record CreateRoomCommand(
    Guid HotelId,
    RoomType Type,
    decimal Taxes,
    string Location
) : IRequest<Guid>;