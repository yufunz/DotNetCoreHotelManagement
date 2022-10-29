using System.ComponentModel.DataAnnotations;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomNum { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public RoomStatus Status { get; set; }
        public RoomType RoomType { get; set; }
    }
}
