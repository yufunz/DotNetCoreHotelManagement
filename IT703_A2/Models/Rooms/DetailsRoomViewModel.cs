using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Rooms
{
    public class DetailsRoomViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Room name")]
        public string RoomNum { get; set; }

        public int FloorNum { get; set; }

        public string Description { get; set; }

        [Display(Name = "Hotel name")]
        public string HotelName { get; set; }

        [Display(Name = "Room type")]
        public string RoomType { get; set; }

        [Display(Name = "Room status")]
        public string RoomStatus { get; set; }
    }
}
