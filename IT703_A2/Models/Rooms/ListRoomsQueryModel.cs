namespace IT703_A2.Models.Rooms
{
    public class ListRoomsQueryModel
    {
        public ListRoomsQueryModel()
        {
            Rooms = new List<ListRoomsViewModel>();
            CurrentPage = 1;
            ItemsOnPage = 10;
        }

        public int CurrentPage { get; set; }

        public int NextPage { get; set; }

        public int PreviousPage { get; set; }

        public int TotalItems { get; set; }

        public int ItemsOnPage { get; set; }

        public string Search { get; set; }

        public IEnumerable<ListRoomsViewModel> Rooms { get; set; }
    }
}
