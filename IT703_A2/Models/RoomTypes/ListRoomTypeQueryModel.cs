namespace IT703_A2.Models.RoomTypes
{
    public class ListRoomTypeQueryModel
    {
        public ListRoomTypeQueryModel()
        {
            RoomTypes = new List<ListRoomTypeViewModel>();
            ItemsPerPage = 3;
            CurrentPage = 1;
        }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ListRoomTypeViewModel> RoomTypes { get; set; }
    }
}
