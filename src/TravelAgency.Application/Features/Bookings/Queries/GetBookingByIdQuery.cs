using MediatR;
using TravelAgency.Domain.Entities;


namespace TravelAgency.Application.Features.Bookings.Queries;

public record GetBookingByIdQuery(
    Guid BookingId
) : IRequest<Booking>;