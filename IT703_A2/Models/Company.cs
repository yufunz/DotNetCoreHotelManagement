﻿using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Detail { get; set; }
        public ICollection<Guest> Guests { get; set; }
    }
}
