using System.ComponentModel.DataAnnotations;

namespace HotelAPIMinimal.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
