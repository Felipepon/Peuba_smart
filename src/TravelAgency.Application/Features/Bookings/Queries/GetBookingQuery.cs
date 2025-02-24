
using MediatR;
using TravelAgency.Application.DTOs;

namespace TravelAgency.Application.Features.Bookings.Queries
{
    public record GetBookingQuery(Guid Id) : IRequest<BookingDetailsResponse>;
}