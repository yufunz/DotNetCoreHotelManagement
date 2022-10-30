using IT703_A2.Models.Invoices;

namespace IT703_A2.Services
{
    public interface IInvoicesService
    {
        AllInvoicesQueryModel All(AllInvoicesQueryModel query);

        void Pay(string id);

        DetailsInvoiceViewModel Details(string id);

        void Delete(string id);
    }
}
