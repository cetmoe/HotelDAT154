﻿

using System.ComponentModel.DataAnnotations;

namespace HotelAPIMinimal.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; } = null!;
        [Required]
        public Room Room { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}
