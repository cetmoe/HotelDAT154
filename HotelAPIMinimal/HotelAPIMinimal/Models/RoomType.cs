using System.ComponentModel.DataAnnotations;

namespace HotelAPIMinimal.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        [Required]
        public string RoomSize { get; set; }

        [Required]
        public int Beds { get; set; }

        [Required]
        public int Price { get; set; }
    }
}