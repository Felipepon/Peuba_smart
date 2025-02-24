using MediatR;
using TravelAgency.Application.DTOs;

namespace TravelAgency.Application.Features.Bookings.Commands
{
    public record CreateBookingCommand(
        Guid RoomId,
        DateTime CheckInDate,
        DateTime CheckOutDate,
        List<GuestDto> Guests,
        EmergencyContactDto EmergencyContact
    ) : IRequest<Guid>;
}
