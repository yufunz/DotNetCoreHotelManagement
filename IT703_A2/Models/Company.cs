using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Detail { get; set; }
        public ICollection<Guest> Guests { get; set; }
    }
}
