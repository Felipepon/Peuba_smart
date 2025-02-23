using MediatR;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Commands;

public record CreateHotelCommand(
    string Name,
    string City
) : IRequest<Guid>;