using IT703_A2.Models;
using IT703_A2.Models.Guests;
using IT703_A2.Data;
using IT703_A2.Models.Enums;

namespace IT703_A2.Services
{
    public class GuestsService : IGuestsService
    {
        private readonly ApplicationDbContext db;

        public GuestsService(ApplicationDbContext dBase)
        {
            db = dBase;
        }

        public async Task Add(AddGuestFormModel guest)
        {
            var newGuest = new Guest
            {
                Address = guest.Address,
                Details = guest.Details,
                Email = guest.Email,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Phone = guest.Phone,
                Agency = guest.Agency,
                Company = guest.Company,
                CreatedAt = DateTime.Now
            };

            await db.Guests.AddAsync(newGuest);
            await db.SaveChangesAsync();
        }

        public ListGuestsQueryModel GetGuests(ListGuestsQueryModel query)
        {
            var dBase = db.Guests.Where(g => g.Deleted == false);

            dBase = Search(query, dBase);
            dBase = Sort(query, dBase);

            var allPages = (int)Math.Ceiling((double)dBase.ToList().Count / query.ItemsPerPage);

            if (query.CurrentPage > allPages)
            {
                query.CurrentPage = allPages;
            }
            if (query.CurrentPage <= 0)
            {
                query.CurrentPage = 1;
            }

            var allGuests = dBase
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(g => new ListGuestsViewModel
                {
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Phone = g.Phone,
                    Id = g.Id,
                    CreatedAt = g.CreatedAt.Date,
                })
                .ToList();

            var guestQueryModel = new ListGuestsQueryModel
            {
                CurrentPage = query.CurrentPage,
                AllGuests = allGuests,
                TotalPages = (int)Math.Ceiling((double)dBase.ToList().Count / query.ItemsPerPage),
                AscOrDesc = query.AscOrDesc
            };

            guestQueryModel.SortBy = query.SortBy;
            guestQueryModel.Search = query.Search;

            return guestQueryModel;
        }

        private IQueryable<Guest> Search(ListGuestsQueryModel query, IQueryable<Guest> currentDb)
        {
            if (string.IsNullOrWhiteSpace(query.Search))
            {
                return currentDb;
            }

            return currentDb
                .Where(g => (g.FirstName.ToLower() + " " + g.LastName.ToLower()).Contains(query.Search.ToLower()) ||
                        g.Phone.ToLower().Contains(query.Search.ToLower()) ||
                        g.Email.ToLower().Contains(query.Search.ToLower()) ||
                        g.Address.ToLower().Contains(query.Search.ToLower()));
        }

        private IQueryable<Guest> Sort(ListGuestsQueryModel query, IQueryable<Guest> dbase)
        {
            switch (query.SortBy)
            {
                case SortBy.None:
                    return dbase.OrderByDescending(g => g.CreatedAt);
                case SortBy.FName:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.FirstName)
                        : dbase.OrderByDescending(g => g.FirstName);
                case SortBy.LName:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.LastName)
                            : dbase.OrderByDescending(g => g.LastName);
                case SortBy.Phone:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.Phone)
                            : dbase.OrderByDescending(g => g.Phone);
                case SortBy.CreatedAt:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.CreatedAt)
                            : dbase.OrderByDescending(g => g.CreatedAt);
                default:
                    return dbase.OrderByDescending(g => g.CreatedAt);
            }
        }

        public DetailsGuestViewModel Details(string id)
        {
            return db.Guests
                .Where(g => g.Id == id)
                .Select(g => new DetailsGuestViewModel
                {
                    FirstName = g.FirstName,
                    Address = g.Address,
                    CreatedAt = g.CreatedAt.ToString("dd-MM-yyyy"),
                    CreatedBookingsCount = g.Bookings.Where(r => r.Status == BookingStatus.Confirmed).Count(),
                    Details = g.Details,
                    Email = g.Email,
                    LastName = g.LastName,
                    Phone = g.Phone,
                    Id = g.Id,
                }).FirstOrDefault();
        }

        public async Task Delete(string id)
        {
            await ChangeBookingStatus(id);

            var guest = db.Guests.Where(g => g.Id == id).FirstOrDefault();

            guest.Deleted = true;

            db.Update(guest);
            await db.SaveChangesAsync();
        }

        private async Task ChangeBookingStatus(string id)
        {
            db.Guests.Where(g => g.Id == id);

            var bookings = db
                .Bookings
                .Where(r => r.GuestId == id && r.CheckOut >= DateTime.Now.Date)
                .ToList();

            var allInvoices = db
                .Invoices
                .Where(i => i.Status != InvoiceStatus.Canceled)
                .ToList();

            foreach (var booking in bookings)
            {
                booking.Status = BookingStatus.Canceled;
                ChangeInvoiceStatus(booking, allInvoices);
            }

            db.Bookings.UpdateRange(bookings);
            await db.SaveChangesAsync();
        }

        private void ChangeInvoiceStatus(Booking booking, IEnumerable<Invoice> invoices)
        {
            if (invoices.Any(a => a.BookingId == booking.Id))
            {
                var curInvoice = invoices.FirstOrDefault(i => i.BookingId == booking.Id);
                curInvoice.Status = InvoiceStatus.Canceled;
            }
        }

        public EditGuestFormModel GetGuest(string id)
        {
            var editGuest = this.db
                .Guests
                .Where(g => g.Id == id)
                .Select(g => new EditGuestFormModel
                {
                    Address = g.Address,
                    Details = g.Details,
                    Email = g.Email,
                    FirstName = g.FirstName,
                    Id = g.Id,
                    LastName = g.LastName,
                    Phone = g.Phone,
                })
                .FirstOrDefault();

            return editGuest;
        }

        public async Task Edit(EditGuestFormModel guest)
        {
            var currentGuest = db
                .Guests.FirstOrDefault(g => g.Id == guest.Id);

            currentGuest.FirstName = guest.FirstName;
            currentGuest.Address = guest.Address;
            currentGuest.Details = guest.Details;
            currentGuest.Email = guest.Email;
            currentGuest.LastName = guest.LastName;
            currentGuest.Phone = guest.Phone;

            db.Update(currentGuest);
            await db.SaveChangesAsync();
        }
    }
}
