using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.LoginUsers
{
    public class LoginUsersFormModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
