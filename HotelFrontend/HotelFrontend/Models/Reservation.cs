

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelFrontend.Models
{
    public class Reservation
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("user")]
        public User User { get; set; }

        [Required]
        [JsonPropertyName("room")]
        public Room Room { get; set; }

        [DataType(DataType.Date)]
        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        [JsonPropertyName("to")]
        public DateTime To { get; set; }
    }
}
