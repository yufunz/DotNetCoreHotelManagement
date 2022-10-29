using System.ComponentModel.DataAnnotations;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models
{
    public class Type
    {
        public int RoomTypeId { get; set; }
        [Required]
        public RoomType RoomType { get; set; }
        public string Description { get; set; }
        public int MaxGuests { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
