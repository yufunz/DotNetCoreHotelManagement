namespace IT703_A2.Models.Guests
{
    public class ListGuestsQueryModel
    {
        public ListGuestsQueryModel()
        {
            AllGuests = new List<ListGuestsViewModel>();
            CurrentPage = 1;
            ItemsPerPage = 10;
        }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }

        public SortBy SortBy { get; set; }

        public int AscOrDesc { get; set; }

        public string Search { get; set; }

        public IEnumerable<ListGuestsViewModel> AllGuests { get; set; }
    }
}
