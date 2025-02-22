using MediatR;

namespace TravelAgency.Application.Features.Hotels.Commands;

public record CreateHotelCommand(
    string Name,
    string City
) : IRequest<Guid>;