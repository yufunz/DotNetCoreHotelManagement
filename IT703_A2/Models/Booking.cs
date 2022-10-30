using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models
{
    public class Booking
    {
        public Booking()
        {
            RoomBookeds = new HashSet<RoomBooked>();
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Notes { get; set; }
        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Rate { get; set; }
        public BookingStatus Status { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal RestaurantCharge { get; set; }
        [Required]
        public bool Paid { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        public int Duration { get; set; }
        public string GuestId { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual ICollection<RoomBooked> RoomBookeds { get; set; }
        public virtual Carpark? Carpark { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
