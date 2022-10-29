using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
    }
}
