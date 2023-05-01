using System.ComponentModel.DataAnnotations;

namespace HotelAPIMinimal.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}