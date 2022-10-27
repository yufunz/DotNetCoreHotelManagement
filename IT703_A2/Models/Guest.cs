using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        public int NumChildren { get; set; }
        public int NumAdults { get; set; }
        public Agency Agency { get; set; }
        public Company Company { get; set; }
    }
}
