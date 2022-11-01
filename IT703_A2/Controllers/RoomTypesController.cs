using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IT703_A2.Data;
using IT703_A2.Models;
using IT703_A2.Models.RoomTypes;
using IT703_A2.Services;

namespace IT703_A2.Controllers
{
    [Authorize(Roles = "Manager,Reception")]
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypesService roomTypeService;

        public RoomTypesController(IRoomTypesService rTService)
        {
            this.roomTypeService = rTService;
        }

        public IActionResult All([FromQuery] ListRoomTypeQueryModel rTQuery)
        {
            var allRoomTypes = this.roomTypeService.ListTypes(rTQuery);

            return this.View(allRoomTypes);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomTypeFormModel room)
        {
            if (!ModelState.IsValid)
            {
                return this.View(room);
            }

            await this.roomTypeService.Add(room);

            return this.RedirectToAction("All", "RoomTypes");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.roomTypeService.Delete(id);

            return this.RedirectToAction("All", "RoomTypes");
        }

        public IActionResult Edit(string id)
        {
            var currentType = this.roomTypeService.GetRoomType(id);

            return this.View(currentType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoomTypeFormModel rType)
        {
            if (!ModelState.IsValid)
            {
                return this.View(rType);
            }

            await this.roomTypeService.Update(rType);

            return this.RedirectToAction("All", "RoomTypes");
        }

    }
}
