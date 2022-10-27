using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        [Required]
        public Type Type { get; set; }
        public string Description { get; set; }
        public int MaxGuests { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }

    public enum Type
    {
        Single, TwoBed, Superior
    }
}
