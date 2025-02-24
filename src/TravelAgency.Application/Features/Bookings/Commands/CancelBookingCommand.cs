using MediatR;


namespace TravelAgency.Application.Features.Bookings.Commands;

public record CancelBookingCommand(
    Guid BookingId
) : IRequest<Unit>;