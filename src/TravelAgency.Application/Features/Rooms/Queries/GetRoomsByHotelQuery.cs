// src/TravelAgency.Application/Features/Rooms/Queries/GetRoomsByHotelQuery.cs
using MediatR;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Features.Rooms.Queries;

public record GetRoomsByHotelQuery(
    Guid HotelId
) : IRequest<List<Room>>;