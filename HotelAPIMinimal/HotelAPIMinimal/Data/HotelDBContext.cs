using HotelAPIMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAPIMinimal.Data
{
    public class HotelDBContext : DbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options) { }

        public DbSet<Room> Room { get; set; } = null!;
        public DbSet<RoomType> RoomType { get; set; } = null!;
        public DbSet<Reservation> Reservation { get; set; } = null!;
        public DbSet<ServiceTask> ServiceTask { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
    }
}
