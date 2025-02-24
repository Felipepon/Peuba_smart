
namespace TravelAgency.Domain.Enums;

public static class RoomTypeDetails
{
    public static readonly Dictionary<RoomType, (decimal BaseCost, int Capacity)> Details = new()
    {
        { RoomType.Standard, (BaseCost: 100, Capacity: 2) },
        { RoomType.Deluxe,   (BaseCost: 200, Capacity: 4) },
        { RoomType.Suite,    (BaseCost: 300, Capacity: 6) }
    };

    public static decimal GetBaseCost(RoomType type) => Details[type].BaseCost;
    public static int GetCapacity(RoomType type) => Details[type].Capacity;
}