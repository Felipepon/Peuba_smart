namespace TravelAgency.Application.DTOs
{
    public record GuestDto(
        string FullName,
        DateTime BirthDate,
        string Gender, 
        string DocumentType,
        string DocumentNumber,
        string Email,
        string Phone
    );

    public record EmergencyContactDto(
        string FullName,
        string Phone
    );
}
