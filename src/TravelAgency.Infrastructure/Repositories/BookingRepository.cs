
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Data;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(Guid id, params Expression<Func<Booking, object>>[] includes)
    {
        var query = _context.Bookings.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Booking>> GetBookingsByHotelIdAsync(Guid hotelId)
    {
        return await _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.Room.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync(); 
    }

    public Task UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Booking booking)
    {
        _context.Bookings.Remove(booking);
        return Task.CompletedTask;
    }
    public async Task<List<Booking>> GetBookingsByRoomAsync(Guid roomId)
    {
        return await _context.Bookings
            .Where(b => b.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<EmergencyContact> GetEmergencyContactAsync(Guid bookingId)
    {
        return await _context.EmergencyContacts
            .FirstOrDefaultAsync(ec => ec.Booking.Id == bookingId); 
    }
}