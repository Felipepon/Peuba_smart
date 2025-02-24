using MediatR;
using TravelAgency.Domain.Enums;


namespace TravelAgency.Application.Features.Rooms.Commands
{
    public record CreateRoomCommand(
        RoomType Type,
        decimal BaseCost,
        decimal Taxes,
        string Location
    ) : IRequest<Guid>
    {
        public Guid HotelId { get; init; }
    }
}
