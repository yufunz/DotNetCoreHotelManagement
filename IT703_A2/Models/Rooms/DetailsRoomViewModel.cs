using System.ComponentModel.DataAnnotations;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models.Rooms
{
    public class DetailsRoomViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Room Name")]
        public string RoomNum { get; set; }

        [Display(Name = "Floor Number")]
        public int FloorNum { get; set; }

        public string Description { get; set; }

        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }

        [Display(Name = "Room Type")]
        public string RoomType { get; set; }

        [Display(Name = "Room Status")]
        public RoomStatus RoomStatus { get; set; }
    }
}
