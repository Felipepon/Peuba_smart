// src/TravelAgency.Infrastructure/Repositories/RoomRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Data;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
    {
        return await _context.Rooms
            .Where(r => r.IsEnabled && 
                !r.Bookings.Any(b => 
                    b.CheckInDate < checkOut && 
                    b.CheckOutDate > checkIn))
            .ToListAsync();
    }

    public async Task<List<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, int guests, string city)
    {
        return await _context.Rooms
            .Where(r => r.IsEnabled && 
                r.MaxGuests >= guests && // <-- Cambiar a MaxGuests
                r.City == city && 
                !r.Bookings.Any(b => 
                    b.CheckInDate < checkOut && 
                    b.CheckOutDate > checkIn))
            .ToListAsync();
    }

    public async Task<Room> GetByIdAsync(Guid id)
    {
        return await _context.Rooms
            .Include(r => r.Hotel)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Room room)
{
    await _context.Rooms.AddAsync(room);
    await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
}

    public Task UpdateAsync(Room room)
    {
        _context.Rooms.Update(room);
        return Task.CompletedTask;
    }
    public async Task DeleteAsync(Room room) => _context.Rooms.Remove(room);

    public async Task<List<Room>> GetRoomsByHotelIdAsync(Guid hotelId)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();
    }
}