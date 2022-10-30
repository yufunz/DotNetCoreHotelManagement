using IT703_A2.Data;
using IT703_A2.Models;
using IT703_A2.Models.Enums;
using IT703_A2.Models.Bookings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT703_A2.Services
{
    public class BookingsService : IBookingsService
    {
        private readonly ApplicationDbContext db;

        public BookingsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        
        public async Task AddBooking(AddBookingFormModel booking)
        {
            var allBookedRooms = this.GetBookedRooms(booking);

            var curBooking = new Booking
            {
                Duration = booking.CheckOut.Subtract(booking.CheckIn).Days,
                CheckOut = booking.CheckOut,
                Name = booking.Name,
                RoomBookeds = allBookedRooms.Select(r => new RoomBooked
                {
                    RoomId = r.Id
                }).ToList(),
                CheckIn = booking.CheckIn,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Rate = allBookedRooms.Select(r => r.Rate).Sum() * booking.CheckOut.Subtract(booking.CheckIn).Days
            };

            await this.db.Bookings.AddAsync(curBooking);
            await this.db.SaveChangesAsync();
        }

        public BookingsQueryModel All(BookingsQueryModel booking)
        {
            //Get distincted booking ids
            var bookings = this.db
                .RoomBookeds
                .Where(r => r.Booking.Status != BookingStatus.Canceled)
                .Select(r => r.BookingId)
                .Distinct();


            var bookingDb = this.db
               .RoomBookeds
               .Where(r => r.Booking.Status != BookingStatus.Canceled)
               .Select(r => r.Booking)
               .AsQueryable();

            var reservCol = new List<BookingsViewModel>();

            foreach (var bk in bookings)
            {
                var tempBooking = bookingDb
                .Where(r => r.Id == bk)
                .FirstOrDefault();

                reservCol.Add(new BookingsViewModel
                {
                    Name = tempBooking.Name,
                    CheckOut = tempBooking.CheckOut.ToString("dd-MM-yyyy"),
                    Id = tempBooking.Id,
                    CheckIn = tempBooking.CheckIn.ToString("dd-MM-yyyy"),
                    Status = tempBooking.Status.ToString(),
                    CreatedAt = tempBooking.CreatedAt
                });
            }

            if (!string.IsNullOrWhiteSpace(booking.Search))
            {
                reservCol = reservCol
                    .Where(r => r.Name.ToLower().Contains(booking.Search.ToLower()))
                    .ToList();
            }

            var tPages = (int)Math.Ceiling((double)reservCol.Count() / booking.ItemsPerPage);

            if (booking.CurrentPage > tPages)
            {
                booking.CurrentPage = tPages;
            }

            if (booking.CurrentPage <= 0)
            {
                booking.CurrentPage = 1;
            }


            var allBookings = reservCol
                .OrderByDescending(r => r.CreatedAt)
                .Skip((booking.CurrentPage - 1) * booking.ItemsPerPage)
                .Take(booking.ItemsPerPage)
                .ToList();

            var reservetionsQueryModel = new BookingsQueryModel
            {
                Bookings = allBookings,
                TotalPages = tPages,
                CurrentPage = booking.CurrentPage,
                PreviousPage = booking.PreviousPage,
                NextPage = booking.NextPage,
                Search = !string.IsNullOrWhiteSpace(booking.Search) ? booking.Search : ""
            };

            return reservetionsQueryModel;
        }

        public async Task AssignGuestToBooking(AssignGuestFormModel guest)
        {
            var currentBooking = this.db
                .Bookings
                .FirstOrDefault(r => r.Id == guest.BookingId);

            var currentGuest = this.db
                .Guests
                .Select(g => new
                {
                    Id = g.Id,
                })
                .FirstOrDefault();

            currentBooking.GuestId = currentGuest.Id;
            currentBooking.Status = BookingStatus.Confirmed;
            currentBooking.Invoice = this.CreateInvoice(currentBooking);

            this.db.Bookings.Update(currentBooking);
            await this.db.SaveChangesAsync();
        }

        private Invoice CreateInvoice(Booking booking)
        {
            return new Invoice
            {
                Amount = booking.Rate,
                Booking = booking,
                IssuedDate = DateTime.Now,
                Paid = false,
                Status = InvoiceStatus.Pending,
            };

        }

        private async Task DeleteInvoice(string id)
        {
            var invoiceForDelete = this.db
                .Invoices.OrderBy(i => i.IssuedDate)
                .FirstOrDefault(i => i.BookingId == id);

            if (invoiceForDelete != null)
            {
                invoiceForDelete.Status = InvoiceStatus.Canceled;

                this.db.Invoices.Update(invoiceForDelete);
                await this.db.SaveChangesAsync();
            }
        }

        private decimal CalculatePrice(int percent, decimal price)
        {
            var discountedPrice = 1 - ((decimal)percent / 100);

            return discountedPrice * price;
        }

        public async Task CancelBooking(string roomId)
        {
            var today = DateTime.Now.Date;

            var allBookings = this.db
                .RoomBookeds
                .Where(r => r.RoomId == roomId && r.Booking.CheckIn >= today)
                .Select(r => r.Booking)
                .ToList();

            if (allBookings.Count > 0)
            {
                foreach (var roomBooked in allBookings)
                {
                    roomBooked.Status = BookingStatus.Canceled;
                }

                this.db.UpdateRange(allBookings);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task Delete(string id)
        {
            var booking = this.db.Bookings.FirstOrDefault(r => r.Id == id);

            booking.Status = BookingStatus.Canceled;

            this.db.Bookings.Update(booking);
            await this.db.SaveChangesAsync();

            await this.DeleteInvoice(id);
        }

        public DetailsBookingViewModel GetDetails(string id)
        {
            return this.db
                .Bookings
                .Where(r => r.Id == id)
                .Select(r => new DetailsBookingViewModel
                {
                    CreatedAt = r.CreatedAt.ToString("dd-MM-yyyy"),
                    Duration = r.Duration,
                    CheckOut = r.CheckOut.ToString("dd-MM-yyyy"),
                    Id = r.Id,
                    Name = r.Name,
                    Price = r.Rate,
                    CheckIn = r.CheckIn.ToString("dd-MM-yyyy"),
                    Status = r.Status.ToString(),
                    GuestName = r.Guest.FirstName + " " + r.Guest.LastName,
                    InvoicePaid = r.Invoice.Paid ? "Yes" : "No",
                    RoomBookeds = string.Join(", ", r.RoomBookeds
                        .Select(rr => rr.Room.RoomNum)
                        .ToList())
                }).FirstOrDefault();
        }

        public AddBookingFormModel ListFreeRooms(AddBookingFormModel booking)
        {
            if (booking.CheckIn < booking.CheckOut &&
                booking.CheckIn >= DateTime.Now.Date && booking.CheckOut > DateTime.Now.Date)

            {
                var freeRooms = this.db
                    .Rooms
                    .Where(r => r.IsDeleted == false)
                    .Select(r => new
                    {
                        RoomName = r.RoomNum,
                        RoomId = r.Id,
                        RoomType = r.RoomType.Name.ToString(),
                        HasBooking = r.RoomBookeds
                            .Where(rr => rr.Booking.Status != BookingStatus.Canceled)
                            .Any(rr => booking.CheckIn < rr.Booking.CheckOut && booking.CheckOut > rr.Booking.CheckIn)
                    })
                    .Where(r => r.HasBooking == false)
                    .ToList();


                var allAvailRooms = freeRooms.Select(i => new SelectListItem
                {
                    Text = i.RoomName + " " + i.RoomType,
                    Value = i.RoomId
                });

                booking.AvailableRooms = allAvailRooms
                    .OrderBy(r => r.Text)
                    .ToList();
            }

            return booking;
        }

        public AssignGuestFormModel LoadGuest(string identityId)
        {
            return this.db
                .Guests
                .Where(g => g.Deleted == false)
                .Select(g => new AssignGuestFormModel
                {
                    GuestAddress = g.Address,
                    GuestId = g.Id,
                    GuestName = g.FirstName + " " + g.LastName,
                    GuestPhone = g.Phone,
                    LoadGuestButton = "Load Guest"
                })
                .FirstOrDefault();
        }

        private IEnumerable<RoomsServiceModel> GetBookedRooms(AddBookingFormModel booking)
        {
            var allBookedRooms = new List<RoomsServiceModel>();

            foreach (var roomId in booking.SelectedRooms)
            {
                var curRoom = this.db
                    .Rooms
                    .Where(r => r.Id == roomId)
                    .Select(r => new RoomsServiceModel
                    {
                        Id = r.Id,
                        Number = r.RoomNum,
                        Rate = r.RoomType.Rate
                    })
                    .FirstOrDefault();

                allBookedRooms.Add(curRoom);
            }

            return allBookedRooms;
        }

        public Hotel GetHotel()
        {
            return this.db
                .Hotels
                .FirstOrDefault();
        }
    }
}
