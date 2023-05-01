
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelFrontend.Models
{
    public class ServiceTask
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [DataType(DataType.Date)]
        [JsonPropertyName("scheduledDate")]
        public DateTime ScheduledDate { get; set; }

        [Required]
        [JsonPropertyName("room")]
        public Room Room { get; set; }
    }
}