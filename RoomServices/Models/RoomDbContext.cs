using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoomServices.Models;

public partial class RoomDbContext : DbContext
{
    public RoomDbContext()
    {
    }

    public RoomDbContext(DbContextOptions<RoomDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=INBLRVM26590142;Initial Catalog=HotelManagementSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
