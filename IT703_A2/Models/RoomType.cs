using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT703_A2.Models
{
    public class RoomType
    {
        public RoomType()
        {
            Id = Guid.NewGuid().ToString();
            Rooms = new HashSet<Room>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Rate { get; set; }
        public int NumOfBeds { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
