using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT703_A2.Models.RoomTypes
{
    public class ListRoomTypeViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Rate { get; set; }

        public int NumOfBeds { get; set; }

        public string Image { get; set; }

        public int RoomsCount { get; set; }
    }
}
