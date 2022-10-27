using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomNum { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public Status Status { get; set; }
        public RoomType RoomType { get; set; }
    }

    public enum Status
    {
        VacantClean, VacantDirty, OccupiedClean, OccupiedService, OnMaintenance
    }
}
