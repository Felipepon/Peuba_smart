// src/TravelAgency.Domain/Interfaces/IRoomRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
    Task<List<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, int guests, string city); // <-- Agregar esta línea
    Task<Room> GetByIdAsync(Guid id);
    Task AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(Room room);
    Task<List<Room>> GetRoomsByHotelIdAsync(Guid hotelId); // <-- Agregar esta línea
}