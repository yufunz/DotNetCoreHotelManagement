using IT703_A2.Models.Bookings;
using IT703_A2.Models;

namespace IT703_A2.Services
{
    public interface IBookingsService
    {
        BookingsQueryModel All(BookingsQueryModel booking);

        Task CancelBooking(string roomId);

        AddBookingFormModel ListFreeRooms(AddBookingFormModel booking);

        Task AddBooking(AddBookingFormModel booking);

        DetailsBookingViewModel GetDetails(string id);

        Task Delete(string id);

        AssignGuestFormModel LoadGuest(string guestId);

        Task AssignGuestToBooking(AssignGuestFormModel guest);

        Hotel GetHotel();
    }
}
