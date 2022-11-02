using System.ComponentModel.DataAnnotations;
using IT703_A2.Models.Enums;

namespace IT703_A2.Models.Rooms
{
    public class AddRoomFormModel
    {
       public AddRoomFormModel()
       {
           RoomTypes = new List<RoomTypeViewModel>();
       }

        [Display(Name = "Room Name")]
        [Required]
        public string RoomNum { get; set; }

        [Display(Name = "Floor Number")]
        [Range(1, 30)]
        [Required]
        public int FloorNum { get; set; }

        public string? Description { get; set; }

        [Required]      
        public string HotelId { get; set; }

        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public string RoomTypeId { get; set; }

        [Required]
        [Display(Name = "Room Status")]
        public RoomStatus RoomStatus { get; set; }

        [Required]
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
