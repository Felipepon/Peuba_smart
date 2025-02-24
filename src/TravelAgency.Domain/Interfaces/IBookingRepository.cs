// src/TravelAgency.Domain/Interfaces/IBookingRepository.cs
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, params Expression<Func<Booking, object>>[] includes);
    Task<List<Booking>> GetBookingsByHotelIdAsync(Guid hotelId);
    Task AddAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(Booking booking);
    Task<List<Booking>> GetBookingsByRoomAsync(Guid roomId);
    Task<EmergencyContact> GetEmergencyContactAsync(Guid bookingId); // <-- Agregar esta línea
}