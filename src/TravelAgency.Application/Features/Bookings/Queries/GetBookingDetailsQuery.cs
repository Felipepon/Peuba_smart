using MediatR;
using System;

using TravelAgency.Application.DTOs;


namespace TravelAgency.Application.Features.Bookings.Queries;

public record GetBookingDetailsQuery(
    Guid BookingId
) : IRequest<BookingDetailsResponse>;