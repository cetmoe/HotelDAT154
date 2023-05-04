using System.ComponentModel.DataAnnotations;

namespace HotelAPIMinimal.Models
{
    public class ServiceTask
    {
        public int Id { get; set; }

        [Required]
        public int Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public Room Room { get; set; } = null!;

        [Required]
        public int Status { get; set; } = 0;

        public string? Note { get; set; } = "";
    }
}