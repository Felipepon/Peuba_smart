// TravelAgency.Domain/Interfaces/IHotelRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;


namespace TravelAgency.Domain.Interfaces;

public interface IHotelRepository
{
    Task<Hotel> GetByIdAsync(Guid id);
    Task AddAsync(Hotel hotel);
    Task UpdateAsync(Hotel hotel);
    Task ToggleStatusAsync(Guid hotelId);
    Task<List<Hotel>> GetAllAsync(); // Método añadido
}