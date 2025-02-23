// TravelAgency.Application/Features/Hotels/Commands/UpdateHotelCommand.cs
using MediatR;

namespace TravelAgency.Application.Features.Hotels.Commands;

public record UpdateHotelCommand(
    Guid Id,
    string Name,
    string City
) : IRequest<Unit>;