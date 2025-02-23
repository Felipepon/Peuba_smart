// TravelAgency.Infrastructure/Repositories/HotelRepository.cs
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Data;


namespace TravelAgency.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Hotel> GetByIdAsync(Guid id)
    {
        return await _context.Hotels.FindAsync(id);
    }

    public async Task AddAsync(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleStatusAsync(Guid hotelId)
    {
        var hotel = await GetByIdAsync(hotelId);
        hotel.ToggleStatus();
        await UpdateAsync(hotel);
    }

    public async Task<List<Hotel>> GetAllAsync()
    {
        return await _context.Hotels.ToListAsync();
    }
}