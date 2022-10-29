using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Carpark
    {
        public Carpark()
        {
            Id = Guid.NewGuid().ToString();

        }
        [Key]
        public string Id { get; set; }
        public bool IsAvailable { get; set; }
        public string Block { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
