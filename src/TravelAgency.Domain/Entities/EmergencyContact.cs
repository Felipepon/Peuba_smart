// src/TravelAgency.Domain/Entities/EmergencyContact.cs
using TravelAgency.Domain.Enums;

namespace TravelAgency.Domain.Entities;
public class EmergencyContact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Guid BookingId { get; set; } // <-- Agregar esta línea
    public Booking Booking { get; set; } = null!; // <-- Agregar esta línea
}