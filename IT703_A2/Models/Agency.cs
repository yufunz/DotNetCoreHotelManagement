using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Agency
    {
        [Key]
        public int AgencyId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Detail { get; set; }
        public ICollection<Guest> Guests { get; set; }
    }
}
