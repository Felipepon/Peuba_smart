namespace TravelAgency.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!; // <-- Inicialización con null!
    public List<Guest> Guests { get; set; } = new();
    public EmergencyContact EmergencyContact { get; set; } = new();
}