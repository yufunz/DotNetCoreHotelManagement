using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Guests
{
    public class AddGuestFormModel
    {
        [Display(Name ="First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"[0-9\s+]*")]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public string Details { get; set; }

        public Company Company { get; set; }

        public Agency Agency { get; set; }
    }
}
