

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelFrontend.Models
{
    public class User
    {
        [JsonPropertyName("userId")]
        public int userId { get; set; }

        [Required]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }
    }
}