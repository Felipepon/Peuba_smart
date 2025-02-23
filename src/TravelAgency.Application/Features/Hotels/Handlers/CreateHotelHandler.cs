using MediatR;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Commands;

public class CreateHotelHandler : IRequestHandler<CreateHotelCommand, Guid>
{
    private readonly IHotelRepository _repository;

    public CreateHotelHandler(IHotelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = new Hotel(request.Name, request.City);
        await _repository.AddAsync(hotel);
        return hotel.Id;
    }
}