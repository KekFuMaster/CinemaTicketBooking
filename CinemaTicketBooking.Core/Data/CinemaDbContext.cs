using CinemaTicketBooking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking.Core.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Theater)
                .WithMany(t => t.Showtimes)
                .HasForeignKey(s => s.TheaterId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.ReservationSeats)
                .WithOne(s => s.Reservation)
                .HasForeignKey(s => s.ReservationId);
            
            modelBuilder.Entity<Reservation>()
                .Navigation(p => p.ReservationSeats)
                .AutoInclude()
                .UsePropertyAccessMode(PropertyAccessMode.PreferProperty);
        }
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationSeat> ReservationSeats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}