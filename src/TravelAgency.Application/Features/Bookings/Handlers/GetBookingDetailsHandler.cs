
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using MediatR;
using TravelAgency.Application.DTOs;
using TravelAgency.Application.Features.Bookings.Queries;

namespace TravelAgency.Application.Features.Bookings.Handlers;
public class GetBookingDetailsHandler : IRequestHandler<GetBookingDetailsQuery, BookingDetailsResponse>
{
    private readonly IBookingRepository _repository;

    public GetBookingDetailsHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookingDetailsResponse> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.BookingId, b => b.Guests, b => b.Room, b => b.EmergencyContact);
        var guests = booking.Guests.Select(g => new GuestResponse(
            g.FullName,
            g.BirthDate,
            g.Gender.ToString(), 
            g.DocumentType.ToString(), 
            g.DocumentNumber,
            g.Email,
            g.Phone
        )).ToList();

        var response = new BookingDetailsResponse(
            booking.Id,
            booking.CheckInDate,
            booking.CheckOutDate,
            booking.TotalCost,
            booking.Room.Hotel.Name,
            booking.Room.Type.ToString(), 
            guests,
            new EmergencyContactResponse(
                booking.EmergencyContact.FullName,
                booking.EmergencyContact.Phone
            )
        );

        return response;
    }
}