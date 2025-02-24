namespace TravelAgency.Application.DTOs
{
    public record GuestDto(
        string FullName,
        DateTime BirthDate,
        string Gender, // También podrías mapearlo a un enum si configuras la conversión
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
