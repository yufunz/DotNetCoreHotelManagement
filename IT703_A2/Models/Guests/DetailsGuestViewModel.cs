using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Guests
{
    public class DetailsGuestViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        public Company Company { get; set; }

        public Agency Agency { get; set; }

        [Display(Name = "Created at")]
        public string CreatedAt { get; set; }

        [Display(Name = "Total bookings")]
        public int CreatedBookingsCount { get; set; }
    }
}
