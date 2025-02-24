using MediatR;
using TravelAgency.Domain.Entities; // 👈 Añadir esta línea
using TravelAgency.Domain.Enums;

namespace TravelAgency.Application.Features.Bookings.Queries;

public record GetAvailableRoomsQuery(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int Guests,
    string City
) : IRequest<List<Room>>;