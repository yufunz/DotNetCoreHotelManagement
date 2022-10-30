using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Guest
    {
        public Guest()
        {
            Bookings = new HashSet<Booking>();
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string Details { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual Agency? Agency { get; set; }
        public virtual Company? Company { get; set; }
    }
}
