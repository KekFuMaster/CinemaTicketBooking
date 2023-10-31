using System.ComponentModel.DataAnnotations;
using CinemaTicketBooking.Common.Dtos;

namespace CinemaTicketBooking.Core.Dtos;
public class MovieDto : BaseDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(1, 600)]
    public int DurationInMinutes { get; set; }

    [Required]
    public string Genre { get; set; }
}
