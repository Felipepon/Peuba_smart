namespace TravelAgency.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Guid RoomId { get; set; }
    public List<Guest> Guests { get; set; } = new();
    public EmergencyContact EmergencyContact { get; set; } = new();
}