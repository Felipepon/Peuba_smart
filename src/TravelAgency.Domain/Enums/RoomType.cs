using System.Text.Json.Serialization;

namespace TravelAgency.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoomType
    {
        Standard,
        Deluxe,
        Suite
    }
}
