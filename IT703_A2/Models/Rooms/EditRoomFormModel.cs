using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Rooms
{
    public class EditRoomFormModel
    {
       public EditRoomFormModel()
       {
           RoomTypes = new List<RoomTypeViewModel>();
       }

        [Required]
        public string Id { get; set; }

        [Required]
        public string RoomNum { get; set; }

        [Range(1, 30)]
        [Required]
        public int FloorNum { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Current room type")]
        public string CurrentRoomTypeId { get; set; }

        [Required]
        [Display(Name = "Current room status")]
        public string RoomStatus { get; set; }

        [Required]
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
