namespace IT703_A2.Models.Bookings
{
    public class BookingsQueryModel
    {
        public BookingsQueryModel()
        {
            Bookings = new List<BookingsViewModel>();
            ItemsPerPage = 10;
            CurrentPage = 1;
        }

        public string Search { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<BookingsViewModel> Bookings;

    }
}
