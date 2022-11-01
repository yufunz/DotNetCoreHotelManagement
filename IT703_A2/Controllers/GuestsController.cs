using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IT703_A2.Data;
using IT703_A2.Models.Guests;
using IT703_A2.Services;

namespace IT703_A2.Controllers
{
    [Authorize]
    public class GuestsController : Controller
    {
        private readonly IGuestsService guestService;

        public GuestsController(IGuestsService gService)
        {
            this.guestService = gService;
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.guestService.Delete(id);

            return this.RedirectToAction("All", "Guests");
        }

        public IActionResult Details(string id)
        {
            var currentClient = this.guestService.Details(id);

            return this.View(currentClient);
        }

        public IActionResult All([FromQuery] ListGuestsQueryModel query)
        {
            var guests = this.guestService.GetGuests(query);

            return this.View(guests);
        }

        public IActionResult Add()
        {
            var guest = new AddGuestFormModel();

            return this.View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGuestFormModel guest)
        {
            if (!ModelState.IsValid)
            {
                return this.View(guest);
            }

            await this.guestService.Add(guest);

            return RedirectToAction("All", "Guests");
        }

        public IActionResult Edit(string id)
        {
            var guest = this.guestService.GetGuest(id);

            return this.View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGuestFormModel guest)
        {
            if (!ModelState.IsValid)
            {
                return this.View(guest);
            }

            await this.guestService.Edit(guest);

            return this.RedirectToAction("All", "Guests");
        }

    }
}
