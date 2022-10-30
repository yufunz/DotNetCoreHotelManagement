using IT703_A2.Models;
using IT703_A2.Data;
using IT703_A2.Models.Home;
using IT703_A2.Models.Enums;

namespace IT703_A2.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext db;
        private readonly IBookingsService bookingService;

        public HomeService(ApplicationDbContext db, IBookingsService bookingService)
        {
            this.db = db;
            this.bookingService = bookingService;
        }

        public HomeViewModel GetDashboardInfo()
        {
            var currentHotel = this.bookingService.GetHotel();

            var totalGuests = this.GetTotalGuests();

            var totalActiveBookings = GetTotalActiveBookings(currentHotel);

            var totalUnpaidVoices = this.GetTotalUnpaidInvoices(currentHotel);

            var checkIns = this.GetCheckIns(currentHotel);

            var checkOuts = GetCheckOuts(currentHotel);

            var currentDashboard = new HomeViewModel
            {
                TotalCheckIn = checkIns,
                TotalCheckOut = checkOuts,
                TotalGuests = totalGuests,
                TotalBookings = totalActiveBookings,
                TotalUnpaidInvoices = totalUnpaidVoices,
            };

            return currentDashboard;
        }

        private int GetCheckOuts(Hotel currentHotel)
        {
            return this.db
                .RoomBookeds
                .Where(r => r.Booking.Status != BookingStatus.Canceled &&
                r.Booking.CheckOut == DateTime.Now.Date && r.Room.Hotel == currentHotel)
                .Count();
        }

        private int GetCheckIns(Hotel currentHotel)
        {
            return this.db
                .RoomBookeds
                .Where(r => r.Booking.Status != BookingStatus.Canceled &&
                r.Booking.CheckIn == DateTime.Now.Date && r.Room.Hotel == currentHotel)
                .Count();
        }

        private int GetTotalUnpaidInvoices(Hotel currentHotel)
        {
            return this.db
                .Invoices
                .Where(i => i.Status != InvoiceStatus.Canceled &&
                i.Paid == false &&
                i.Booking.RoomBookeds.All(r => r.Room.Hotel == currentHotel))
                .Count();
        }

        private int GetTotalActiveBookings(Hotel currentHotel)
        {
            return this.db
                .Bookings
                .Where(r => r.Status != BookingStatus.Canceled &&
                r.RoomBookeds.All(r => r.Room.Hotel == currentHotel) &&
                r.CheckIn <= DateTime.Now.Date && r.CheckOut >= DateTime.Now.Date)
                .Count();
        }

        private int GetTotalGuests()
        {
            return this.db
                .Guests
                .Where(g => g.Deleted == false)
                .Count();
        }

    }
}
