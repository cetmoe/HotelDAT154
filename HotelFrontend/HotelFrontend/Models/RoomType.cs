using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelFrontend.Models
{
    public class RoomType
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [Required]
        [JsonPropertyName("roomSize")]
        [DisplayName("Room Size")]
        public string RoomSize { get; set; }

        [Required]
        [JsonPropertyName("beds")]
        [DisplayName("Number of beds")]
        public int Beds { get; set; }

        [Required]
        [JsonPropertyName("price")]
        [DisplayName("Price")]
        public int Price { get; set; }
    }
}