using HotelVanKeus.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelVanKeus.Data
{
    public class HotelVanKeusContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }

        public HotelVanKeusContext(DbContextOptions options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelVanKeusDB;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationRoom>().HasKey(s => new { s.RoomId, s.ReservationId });
        }
    }
}