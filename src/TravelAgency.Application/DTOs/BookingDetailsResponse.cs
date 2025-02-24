
using MediatR;

namespace TravelAgency.Application.DTOs;

public record BookingDetailsResponse(
    Guid Id,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalCost,
    string HotelName,
    string RoomType,
    List<GuestResponse> Guests,
    EmergencyContactResponse EmergencyContact
);

public record GuestResponse(
    string FullName,
    DateTime BirthDate,
    string Gender,
    string DocumentType,
    string DocumentNumber,
    string Email,
    string Phone
);

public record EmergencyContactResponse(
    string FullName,
    string Phone
);