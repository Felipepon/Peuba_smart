// src/TravelAgency.Domain/Entities/Booking.cs
using TravelAgency.Domain.Enums;

namespace TravelAgency.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public List<Guest> Guests { get; set; } = new();
        public EmergencyContact EmergencyContact { get; set; } = new();
        public bool IsCancelled { get; set; }
        public decimal TotalCost { get; set; }
    }
}