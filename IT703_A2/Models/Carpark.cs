using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Carpark
    {
        [Key]
        public int CarparkId { get; set; }
        public bool IsAvailable { get; set; }
        public string Block { get; set; }
        public Booking? Booking { get; set; }
    }
}
