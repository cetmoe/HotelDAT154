using HotelAPIMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPIMinimal.Data
{
    public class HotelDBContext : DbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options) { }

        public DbSet<Hotel> Hotel { get; set; } = null!;
        public DbSet<RoomType> RoomType { get; set; } = null!;
        public DbSet<Room> Room { get; set; } = null!;
        public DbSet<Reservation> Reservation { get; set; } = null!;
        public DbSet<ServiceTask> ServiceTask { get; set; } = null!;
        public DbSet<Guest> Guest { get; set; } = null!;
    }
}
