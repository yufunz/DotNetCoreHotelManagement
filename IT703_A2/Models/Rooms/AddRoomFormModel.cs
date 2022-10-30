using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Rooms
{
    public class AddRoomFormModel
    {
       public AddRoomFormModel()
       {
           RoomTypes = new List<RoomTypeViewModel>();
       }

        [Required]
        public string RoomNum { get; set; }

        [Range(1, 30)]
        [Required]
        public int FloorNum { get; set; }

        public string Description { get; set; }

        [Required]      
        public string HotelId { get; set; }

        [Display(Name = "Hotel")]
        public string HotelName { get; set; }

        [Required]
        [Display(Name = "Current room type")]
        public string RoomTypeId { get; set; }

        [Required]
        [Display(Name = "Current room status")]
        public string RoomStatus { get; set; }

        [Required]
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
