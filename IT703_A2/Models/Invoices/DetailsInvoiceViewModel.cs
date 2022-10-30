namespace IT703_A2.Models.Invoices
{
    public class DetailsInvoiceViewModel
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string IssuedDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public string Paid { get; set; }

        public decimal Price { get; set; }

        public string BookingName { get; set; }

        public string GuestName { get; set; }

        public string Address { get; set; }
    }
}
