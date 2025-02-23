using MediatR;

namespace TravelAgency.Application.Features.Hotels.Commands;

public record ToggleHotelStatusCommand(Guid Id) : IRequest<Unit>;