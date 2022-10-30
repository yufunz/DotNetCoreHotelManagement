namespace IT703_A2.Models.Bookings
{
    public class DetailsBookingViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public string CreatedAt { get; set; }

        public int Duration { get; set; }

        public decimal? Price { get; set; }

        public string Status { get; set; }

        public string GuestName { get; set; }

        public string RoomBookeds { get; set; }

        public string InvoicePaid { get; set; }
    }
}
