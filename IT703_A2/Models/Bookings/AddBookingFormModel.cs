using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IT703_A2.Models.Bookings
{
    public class AddBookingFormModel : IValidatableObject
    {
        public AddBookingFormModel()
        {
            AvailableRooms = new List<SelectListItem>();
            SelectedRooms = new List<string>();
        }

        [Display(Name = "Booking Name")]
        public string Name { get; set; }

        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check-out Date")]
        public DateTime CheckOut { get; set; }

        public string LoadRoomsButton { get; set; }

        public string AddBookingButton { get; set; }

        public ICollection<SelectListItem> AvailableRooms;

        [Display(Name = "Available Rooms")]
        public ICollection<string> SelectedRooms { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.AddBookingButton))
            {
                yield return new ValidationResult(
                   "Should write name for booking!", new[] { nameof(this.Name) });
            }

            if (this.SelectedRooms.Count == 0 && !string.IsNullOrWhiteSpace(this.AddBookingButton))
            {
                yield return new ValidationResult(
                   "Should select one or more rooms for booking!", new[] { nameof(this.SelectedRooms) });
            }

            if (this.CheckIn >= this.CheckOut || this.CheckIn < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                   "Check-in date can't be greater than check-out date, and check-in date must be from today or later!", new[] { nameof(this.CheckIn) });
            }

            if (this.CheckOut <= this.CheckIn || this.CheckOut <= DateTime.Now.Date)
            {
                yield return new ValidationResult(
                   "Check-out date can't be lower than check-in date, and check-out date must be from tommorow or later!", new[] { nameof(this.CheckOut) });
            }

        }
    }
}
