using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class RoomBooked
    {
        [Required]
        public string RoomId { get; set; }
        public virtual Room Room { get; set; }
        [Required]
        public string BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
