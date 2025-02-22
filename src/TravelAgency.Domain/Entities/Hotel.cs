
using TravelAgency.Domain.Entities;
namespace TravelAgency.Domain.Entities;

public class Hotel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public List<Room> Rooms { get; set; } = new();

    
    public void ToggleStatus() => IsEnabled = !IsEnabled;
}