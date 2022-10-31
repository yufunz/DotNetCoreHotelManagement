using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT703_A2.Data;
using IT703_A2.Models;
using IT703_A2.Models.Rooms;
using IT703_A2.Services;

namespace IT703_A2.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsService roomsService;

        public RoomsController(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        public IActionResult All([FromQuery] ListRoomsQueryModel rms)
        {
            var allRooms = roomsService.All(rms);

            return this.View(allRooms);
        }

        public IActionResult Details(string id)
        {
            var currentRoom = this.roomsService.Details(id);

            return this.View(currentRoom);
        }

        public IActionResult Edit(string id)
        {
            var room = this.roomsService.Edit(id);

            return this.View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoomFormModel room)
        {
            if(!ModelState.IsValid)
            {
                room.RoomTypes = this.roomsService.GetRoomTypes();
       
                return this.View(room);
            }

            await this.roomsService.Update(room);

            return this.RedirectToAction("All", "Rooms");
        }

        public IActionResult Add()
        {
            var room = this.roomsService.FillRoomAddForm();

            return this.View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomFormModel room)
        {
            if(!ModelState.IsValid)
            { 
                var fillRoomFields = this.roomsService.FillRoomAddForm();

                room.HotelId = fillRoomFields.HotelId;
                room.RoomTypes = fillRoomFields.RoomTypes;
                room.HotelName = fillRoomFields.HotelName;

                return this.View(room);
            }
            
            await this.roomsService.Add(room);

            return this.RedirectToAction("All", "Rooms");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await roomsService.Delete(id);

            return this.RedirectToAction("All", "Rooms");
        }

    }
}
