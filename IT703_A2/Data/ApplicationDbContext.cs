using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IT703_A2.Models;

namespace IT703_A2.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomBooked> RoomsBookeds { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Carpark> Carparks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RoomBooked>(r =>
            {
                r.HasKey(k => new { k.BookingId, k.RoomId });

                r.HasOne(ro => ro.Room)
                .WithMany(rr => rr.RoomBookeds)
                .HasForeignKey(ro => ro.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

    }
}