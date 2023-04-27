using System.ComponentModel.DataAnnotations;

namespace HotelAPIMinimal.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        public Boolean CheckInStatus { get; set; } = false;

        public Boolean CleaningStatus { get; set; } = false;

        [Required]
        public Hotel Hotel { get; set; }
    }
}