using MediatR;

namespace TravelAgency.Application.Features.Rooms.Commands;

public record UpdateRoomCommand(
    Guid Id,
    Guid HotelId,
    decimal Taxes,
    string Location
) : IRequest<Unit>;