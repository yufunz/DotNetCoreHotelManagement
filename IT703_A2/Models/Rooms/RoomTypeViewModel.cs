using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Rooms
{
    public class RoomTypeViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Id { get; set; }
    }
}
