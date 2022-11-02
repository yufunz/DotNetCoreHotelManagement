using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IT703_A2.Data;
using IT703_A2.Models;
using IT703_A2.Models.Bookings;
using IT703_A2.Services;

namespace IT703_A2.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IBookingsService bookingService;

        public BookingsController(IBookingsService bookingService)
        {
            this.bookingService = bookingService;
        }

        public IActionResult All([FromQuery]BookingsQueryModel res)
        {
            var allBookings = bookingService.All(res);

            return this.View(allBookings);
        }

        public IActionResult Add()
        {
            var booking = new AddBookingFormModel();

            booking.CheckIn = DateTime.Now.Date;
            booking.CheckOut = DateTime.Now.AddDays(1);

            booking = this.bookingService.ListFreeRooms(booking);

            return this.View(booking);
        }

        [HttpPost]
        public async Task <IActionResult> Add(AddBookingFormModel booking)
        {

            if (booking.CheckIn < DateTime.Now.Date)
            {
                booking.CheckIn = DateTime.Now.Date;
                booking.CheckOut = DateTime.Now.Date.AddDays(1);
            }

            if(!string.IsNullOrWhiteSpace(booking.LoadRoomsButton))
            {
                booking = this.bookingService.ListFreeRooms(booking);
            }


            if (!string.IsNullOrWhiteSpace(booking.AddBookingButton))
            {

                if (!ModelState.IsValid)
                {
                    booking = this.bookingService.ListFreeRooms(booking);

                    return this.View(booking);
                }

                await this.bookingService.AddBooking(booking);

                return this.RedirectToAction("All", "Bookings");
            }        

            return this.View(booking);
        }

        public IActionResult Details(string id)
        {
            var curBooking = this.bookingService.GetDetails(id);

            return this.View(curBooking);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.bookingService.Delete(id);

            return this.RedirectToAction("All", "Bookings");
        }

        public IActionResult AssignGuest(string id)
        {
            var guest = new AssignGuestFormModel
            {
                BookingId = id,
            };

            return this.View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> AssignGuest(AssignGuestFormModel guest)
        {
            if (!string.IsNullOrWhiteSpace(guest.LoadGuestButton))
            {
                var curGuest = this.bookingService.LoadGuest(guest.GuestId);


                if (curGuest != null)
                {

                    return this.View(curGuest);
                }

                return this.View(guest);
            }

            if(!string.IsNullOrWhiteSpace(guest.AssignButton))
            {
                if (!ModelState.IsValid)
                {
                    return this.View(guest);
                }

                await this.bookingService.AssignGuestToBooking(guest);

                return this.RedirectToAction("All", "Bookings");
            }

            return this.View(guest);
        }
    }
}
