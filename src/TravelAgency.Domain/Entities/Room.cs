
namespace TravelAgency.Domain.Entities;


public class Room : BaseEntity
{
    public RoomType Type { get; set; }
    public decimal BaseCost { get; set; }
    public decimal Taxes { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

   
    public decimal TotalCost => BaseCost + Taxes;

   
    public bool CanAccommodate(int guests) => guests <= RoomTypeDetails.GetCapacity(Type);
}