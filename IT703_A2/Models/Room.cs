using System.ComponentModel.DataAnnotations;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models
{
    public class Room
    {
        public Room()
        {
            Id = Guid.NewGuid().ToString();
            RoomBookeds = new HashSet<RoomBooked>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string RoomNum { get; set; }
        public int FloorNum { get; set; }
        public bool IsDeleted { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public RoomStatus Status { get; set; }
        public RoomType RoomType { get; set; }
        public string HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<RoomBooked> RoomBookeds { get; set; }
    }
}
