
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<EmergencyContact> EmergencyContacts { get; set; } 

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Rooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Room)
            .WithMany()
            .HasForeignKey(b => b.RoomId);

        modelBuilder.Entity<Guest>()
            .HasOne(g => g.Booking)
            .WithMany(b => b.Guests)
            .HasForeignKey(g => g.BookingId);

       
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.EmergencyContact)
            .WithOne(ec => ec.Booking)
            .HasForeignKey<EmergencyContact>(ec => ec.BookingId); 
    }
}