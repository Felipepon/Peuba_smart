
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TravelAgency.Domain.Common;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Entities
{
    public class Room : BaseEntity
    {
        public RoomType Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal Taxes { get; set; }
        public string Location { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
        public Guid HotelId { get; set; }

        [JsonIgnore] 
        public Hotel Hotel { get; set; } = null!;
        
        public List<Booking> Bookings { get; set; } = new();
        public int MaxGuests { get; set; }
        public string City { get; set; } = string.Empty;

        public decimal TotalCost => BaseCost + Taxes;

        public bool CanAccommodate(int guests) => guests <= RoomTypeDetails.GetCapacity(Type);
    }
}