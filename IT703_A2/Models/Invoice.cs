using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models
{
    public class Invoice
    {
        public Invoice()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public InvoiceStatus Status { get; set; }
        public string BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public bool Paid { get; set; }
    }
}
