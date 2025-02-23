using MediatR;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Features.Hotels.Queries;

public record GetHotelByIdQuery(Guid Id) : IRequest<Hotel>;