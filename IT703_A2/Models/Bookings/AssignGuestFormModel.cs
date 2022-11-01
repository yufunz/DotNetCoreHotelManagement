using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Bookings
{
    public class AssignGuestFormModel
    {
        public string GuestId { get; set; }

        public string BookingId { get; set; }

        [Display(Name = "Full Name")]
        public string GuestName { get; set; }

        [Display(Name = "Address")]
        public string GuestAddress { get; set; }

        [Display(Name = "Phone")]
        public string GuestPhone { get; set; }

        public string LoadGuestButton { get; set; }

        public string AssignButton { get; set; }
    }
}
