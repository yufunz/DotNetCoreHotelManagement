using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Bookings
{
    public class AddBookingFormModel
    {
        public AddBookingFormModel()
        {
            AvailableRooms = new List<SelectListItem>();
            SelectedRooms = new List<string>();
        }

        [Display(Name = "Booking name")]
        public string Name { get; set; }

        [Display(Name = "Check-in date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check-out date")]
        public DateTime CheckOut { get; set; }

        public string LoadRoomsButton { get; set; }

        public string AddBookingButton { get; set; }

        public ICollection<SelectListItem> AvailableRooms;

        [Display(Name = "Available rooms")]
        public ICollection<string> SelectedRooms { get; set; }
    }
}
