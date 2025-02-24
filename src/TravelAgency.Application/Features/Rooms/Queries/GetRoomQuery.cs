// src/TravelAgency.Application/Features/Rooms/Queries/GetRoomQuery.cs
using MediatR;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Features.Rooms.Queries
{
    public record GetRoomQuery(Guid HotelId, Guid RoomId) : IRequest<Room>;
}