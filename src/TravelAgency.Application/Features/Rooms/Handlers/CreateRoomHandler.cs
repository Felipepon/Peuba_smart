using MediatR;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Application.Features.Rooms.Commands;


namespace TravelAgency.Application.Features.Rooms.Handlers;

public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, Guid>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;

    public CreateRoomHandler(
        IRoomRepository roomRepository,
        IHotelRepository hotelRepository)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
    }

    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        var baseCost = RoomTypeDetails.GetBaseCost(request.Type); // Usamos la clase de detalles

        var room = new Room
        {
            Type = request.Type,
            BaseCost = baseCost,
            Taxes = request.Taxes,
            Location = request.Location,
            HotelId = request.HotelId
        };

        await _roomRepository.AddAsync(room);
        return room.Id;
    }
}