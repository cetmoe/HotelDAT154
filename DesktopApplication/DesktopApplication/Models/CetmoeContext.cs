using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DesktopApplication.Models;

public partial class CetmoeContext : DbContext
{
    public CetmoeContext()
    {
    }

    public CetmoeContext(DbContextOptions<CetmoeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<ServiceTask> ServiceTasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:cetmoe.database.windows.net,1433;Initial Catalog=cetmoe;User Id=cetmoe@cetmoe;Password=7fgQH.RcGusMrvv");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.HasIndex(e => e.RoomId, "IX_Reservation_RoomId");

            entity.HasIndex(e => e.UserId, "IX_Reservation_UserId");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations).HasForeignKey(d => d.RoomId);

            entity.HasOne(d => d.User).WithMany(p => p.Reservations).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.HasIndex(e => e.RoomTypeId, "IX_Room_RoomTypeId");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms).HasForeignKey(d => d.RoomTypeId);
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.ToTable("RoomType");
        });

        modelBuilder.Entity<ServiceTask>(entity =>
        {
            entity.ToTable("ServiceTask");

            entity.HasIndex(e => e.RoomId, "IX_ServiceTask_RoomId");

            entity.HasOne(d => d.Room).WithMany(p => p.ServiceTasks).HasForeignKey(d => d.RoomId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserName).HasDefaultValueSql("(N'')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
