using IT703_A2.Data;
using IT703_A2.Models.Invoices;
using IT703_A2.Models.Enums;

namespace IT703_A2.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly ApplicationDbContext db;
        private readonly IBookingsService bookingService;

        public InvoicesService (ApplicationDbContext db, IBookingsService bookingService)
        {
            this.db = db;
            this.bookingService = bookingService;
        }

        public AllInvoicesQueryModel All(AllInvoicesQueryModel query)
        {
            var dbInvoices = this.db
                .Invoices
                .Where(i => i.Status != InvoiceStatus.Canceled)
                .Select(o => new
                {
                    Name = o.Booking.Name,
                    Id = o.Id,
                    Paid = o.Paid ? "Yes" : "No",
                    Price = o.Amount,
                    Status = o.Status.ToString(),
                    IssuedDate = o.IssuedDate
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbInvoices = dbInvoices
                    .Where(i => i.Name.ToLower().Contains(query.Search.ToLower()) ||
                        i.Paid.ToLower().Contains(query.Search.ToLower()) ||
                        i.Status.ToLower().Contains(query.Search.ToLower()))
                    .ToList();
            }

            var tPages = (int)Math.Ceiling((double)dbInvoices.Count() / query.ItemsPerPage);

            if (query.CurrentPage > tPages)
            {
                query.CurrentPage = tPages;
            }

            if(query.CurrentPage <= 0)
            {
                query.CurrentPage = 1;
            }

            var allInvoices = dbInvoices
                .OrderByDescending(i => i.IssuedDate)
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(i => new AllInvoicesViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Paid = i.Paid,
                    Price = i.Price,
                    Status = i.Status,
                })
                .ToList();

            var invoicesQueryModel = new AllInvoicesQueryModel
            {
                Invoices = allInvoices,
                Search = query.Search,
                CurrentPage = query.CurrentPage,
                TotalPages = tPages,
                NextPage = query.NextPage,
                PreviousPage = query.PreviousPage
            };

            return invoicesQueryModel;
        }

        public void Delete(string id)
        {
            var currentInvoice = this.db
                .Invoices
                .FirstOrDefault(i => i.Id == id);

            var currentBooking = this.db
                .Bookings
                .FirstOrDefault(r => r.Invoice.Id == id);

            currentInvoice.Status = InvoiceStatus.Canceled;
            currentBooking.Status = BookingStatus.Canceled;

            this.db
                .Invoices
                .Update(currentInvoice);

            this.db
                .Bookings
                .Update(currentBooking);

            this.db.SaveChanges();
        }

        public DetailsInvoiceViewModel Details(string id)
        {
            var currentInvoice = this.db
                .Invoices
                .Where(i => i.Id == id)
                .Select(i => new DetailsInvoiceViewModel
                {
                    Status = i.Status.ToString(),
                    Price = i.Amount,
                    IssuedDate = i.IssuedDate.ToString("dd-MM-yyyy"),
                    PaidDate = i.PaidDate,
                    Paid = i.Paid ? "Yes" : "No",
                    BookingName = i.Booking.Name,
                    GuestName = i.Booking.Guest.FirstName + " " + i.Booking.Guest.LastName,
                    Address = i.Booking.Guest.Address,
                    Id = i.Id,
                })
                .FirstOrDefault();

            return currentInvoice;
        }

        public void Pay(string id)
        {
            var currentInvoice = this.db
                .Invoices
                .FirstOrDefault(i => i.Id == id);

            currentInvoice.Paid = true;
            currentInvoice.PaidDate = DateTime.Now.Date;
            currentInvoice.Status = InvoiceStatus.Active;

            this.db.Invoices.Update(currentInvoice);
            this.db.SaveChanges();
        }
    }
}
