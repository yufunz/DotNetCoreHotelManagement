using IT703_A2.Models.Guests;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IT703_A2.Services
{
    public interface IGuestsService
    {
        Task Edit(EditGuestFormModel guest);

        Task Add(AddGuestFormModel guest);

        ListGuestsQueryModel GetGuests(ListGuestsQueryModel query);

        DetailsGuestViewModel Details(string id);

        EditGuestFormModel GetGuest(string id);

        //ICollection<SelectListItem> GetCompanies();

        //ICollection<SelectListItem> GetAgencies();

        Task Delete(string id);
    }
}
