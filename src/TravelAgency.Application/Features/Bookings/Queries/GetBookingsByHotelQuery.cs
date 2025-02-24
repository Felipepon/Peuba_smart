using MediatR;
using System;
using TravelAgency.Domain.Entities;


namespace TravelAgency.Application.Features.Bookings.Queries;

public record GetBookingsByHotelQuery(
    Guid HotelId
) : IRequest<List<Booking>>;