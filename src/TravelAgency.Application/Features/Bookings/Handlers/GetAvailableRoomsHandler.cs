// src/TravelAgency.Application/Features/Bookings/Handlers/GetAvailableRoomsHandler.cs
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Application.Features.Rooms.Commands;
using TravelAgency.Application.Features.Bookings.Queries;
using MediatR;

namespace TravelAgency.Application.Features.Bookings.Handlers;

public class GetAvailableRoomsHandler : IRequestHandler<GetAvailableRoomsQuery, List<Room>>
{
    private readonly IRoomRepository _roomRepository;

    public GetAvailableRoomsHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<List<Room>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetAvailableRoomsAsync(
            request.CheckInDate,
            request.CheckOutDate,
            request.Guests,
            request.City // <-- Asegúrate de proporcionar todos los argumentos necesarios
        );

        return rooms;
    }
}