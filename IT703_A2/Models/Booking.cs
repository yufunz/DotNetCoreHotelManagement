using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        [Required]
        public int NumGuests { get; set; }
        [Required]
        public double RoomRate { get; set; }
        public double RestaurantCharge { get; set; }
        [Required]
        public bool Paid { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }

        public Guest Guest { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public Carpark? Carpark { get; set; }

    }
}
