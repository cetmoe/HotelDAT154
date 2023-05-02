

using System.ComponentModel;
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
        [DisplayName("From date")]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        [JsonPropertyName("to")]
        [DisplayName("To date")]
        public DateTime To { get; set; }
    }
}
