using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT703_A2.Models.RoomTypes
{
    public class EditRoomTypeFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Range(10, 500)]
        public decimal Rate { get; set; }

        [Range(1,10)]
        [Display(Name = "Number of beds")]
        public int NumOfBeds { get; set; }

        [Url]
        public string Image { get; set; }
    }
}
