using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
