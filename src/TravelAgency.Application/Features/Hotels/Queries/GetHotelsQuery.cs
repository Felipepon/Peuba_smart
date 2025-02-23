using MediatR;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Features.Hotels.Queries;

public record GetHotelsQuery : IRequest<List<Hotel>>;