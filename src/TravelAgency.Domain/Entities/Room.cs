namespace TravelAgency.Domain.Entities;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public RoomType Type { get; set; }
    public decimal BaseCost { get; set; }
    public decimal Taxes { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public Guid HotelId { get; set; }

    // Método para calcular el costo total (base + impuestos)
    public decimal TotalCost => BaseCost + Taxes;
}