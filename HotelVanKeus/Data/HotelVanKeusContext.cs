using HotelVanKeus.Models;
using Microsoft.EntityFrameworkCore;


namespace HotelVanKeus.Data
{
    public class HotelVanKeusContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        //public DbSet<Renovation> Renovations { get; set; }

        public HotelVanKeusContext(DbContextOptions options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelVanKeusDB;Integrated Security=True;");
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Renovation>().HasKey(s => new { s.Id });
            //modelBuilder.Entity<Renovation>().HasKey(s => s.Id );

            //    //Using this one to prevent cascade delete from the Reservations
            //modelBuilder.Entity<Reservation>()
            //    .HasOne(r => r.Room)
            //    .WithMany(r => r.Reservations)
            //    .HasForeignKey("RoomId");
                //.OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Reservation>()
            //    .HasOne(g => g.Guest)
            //    .WithMany(r => r.Reservations)
            //    .HasForeignKey("GuestId");

                //.OnDelete(DeleteBehavior.NoAction);

        //    /*
        //    modelBuilder.Entity<Reservation>()
        //        .HasOne(b => b.Room)
        //        .WithMany(a => a.Reservations)
        //        .OnDelete(DeleteBehavior.NoAction);
        //    */

        }
    
    }
}