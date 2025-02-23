using MediatR;
using TravelAgency.Application.Features.Hotels.Queries;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Handlers;

public class GetHotelsHandler : IRequestHandler<GetHotelsQuery, List<Hotel>>
{
    private readonly IHotelRepository _repository;

    public GetHotelsHandler(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Hotel>> Handle(GetHotelsQuery request, CancellationToken ct)
    {
        return await _repository.GetAllAsync();
    }
}