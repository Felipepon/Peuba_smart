using MediatR;
using TravelAgency.Application.Features.Hotels.Queries;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Handlers;

public class GetHotelByIdHandler : IRequestHandler<GetHotelByIdQuery, Hotel>
{
    private readonly IHotelRepository _repository;

    public GetHotelByIdHandler(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Hotel> Handle(GetHotelByIdQuery request, CancellationToken ct)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}