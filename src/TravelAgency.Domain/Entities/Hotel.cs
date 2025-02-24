
using System.Text.Json.Serialization;
using TravelAgency.Domain.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

    [JsonIgnore] 
    public List<Room> Rooms { get; set; } = new List<Room>();

    
    public Hotel() { }

    
    public Hotel(string name, string city)
    {
        Name = name;
        City = city;
    }

    public void ToggleStatus() => IsEnabled = !IsEnabled;
}