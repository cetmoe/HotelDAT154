using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HotelFrontend.Models
{
    public class Room
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("roomNumber")]
        [DisplayName("Room Number")]
        public int RoomNumber { get; set; }

        [JsonPropertyName("roomType")]
        public RoomType RoomType { get; set; }

        [JsonPropertyName("checkInStatus")]
        public Boolean CheckInStatus { get; set; } = false;
    }
}