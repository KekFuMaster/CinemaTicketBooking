using CinemaTicketBooking.Common.Entitiea;

namespace CinemaTicketBooking.Core.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Movie duration in minutes.
        /// </summary>
        public int Duration { get; set; }
        public string Genre { get; set; }
        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}