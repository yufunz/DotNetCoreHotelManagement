namespace IT703_A2.Models.Invoices
{
    public class AllInvoicesQueryModel
    {
        public AllInvoicesQueryModel()
        {
            CurrentPage = 1;
            ItemsPerPage = 10;
        }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<AllInvoicesViewModel> Invoices { get; set; }
    }
}
