using IT703_A2.Models.Rooms;
using IT703_A2.Data;
using IT703_A2.Models;

namespace IT703_A2.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext db;
        private readonly IBookingsService bookingService;

        public RoomsService(ApplicationDbContext db, IBookingsService bookingService)
        {
            this.db = db;
            this.bookingService = bookingService;
        }

        public ListRoomsQueryModel All(ListRoomsQueryModel rooms)
        {
            var allRoomsDb = this.db.Rooms
                .Where(r => r.IsDeleted == false)
                .OrderBy(r => r.FloorNum)
                .ThenBy(r => r.RoomNum)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(rooms.Search))
            {
                allRoomsDb = allRoomsDb
                    .Where(r => r.RoomNum.ToLower().Contains(rooms.Search.ToLower()) ||
                    r.Description.ToLower().Contains(rooms.Search.ToLower()) ||
                    r.RoomType.Name.ToLower().Contains(rooms.Search.ToLower()));
            }

            var tPages = (int)Math.Ceiling((double)allRoomsDb.Count() / rooms.ItemsOnPage);

            if(rooms.CurrentPage > tPages)
            {
                rooms.CurrentPage = tPages;
            }

            if(rooms.CurrentPage <= 0)
            {
                rooms.CurrentPage = 1;
            }

            var allRooms = allRoomsDb
                .Skip((rooms.CurrentPage - 1) * rooms.ItemsOnPage)
                .Take(rooms.ItemsOnPage)
                .Select(r => new ListRoomsViewModel
                {
                    RoomNum = r.RoomNum,
                    Id = r.Id,
                    RoomType = r.RoomType.Name
                })
                .ToList();

            var roomQueryModel = new ListRoomsQueryModel
            {
                Rooms = allRooms,
                TotalItems = tPages,
                CurrentPage = rooms.CurrentPage,
                PreviousPage = rooms.PreviousPage,
                NextPage = rooms.NextPage
            };

            return roomQueryModel;
        }

        public DetailsRoomViewModel Details(string id)
        {
            var hotel = this.GetHotel();

            var currentRoom = this.db
                .Rooms
                .Where(r => r.IsDeleted == false && r.Id == id)
                .Select(r => new DetailsRoomViewModel
                {
                    Description = r.Description,
                    FloorNum = r.FloorNum,
                    HotelName = hotel.Name,
                    Id = r.Id,
                    RoomNum = r.RoomNum,
                    RoomType = r.RoomType.Name
                })
                .FirstOrDefault();

            return currentRoom;
        }

        public EditRoomFormModel Edit(string id)
        {
            var allRoomTypes = GetRoomTypes();

            var currentRoom = GetRoom(id);

            var roomForEdit = new EditRoomFormModel
            {
                Description = currentRoom.Description,
                FloorNum = currentRoom.FloorNum,
                Id = currentRoom.Id,
                RoomNum = currentRoom.RoomNum,
                RoomTypes = allRoomTypes,
                CurrentRoomTypeId = currentRoom.RoomTypeId
            };

            return roomForEdit;
        }

        private Room GetRoom(string id)
        {
            return this.db
                .Rooms
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<RoomTypeViewModel> GetRoomTypes()
        {
            return this.db
                .RoomTypes
                .Where(rt => rt.IsDeleted == false)
                .Select(rt => new RoomTypeViewModel
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();
        }

        public bool GetRoomNameForEdit(string name, string id)
        {
            var hotel = GetHotel();

            return this.db
                .Rooms
                .Where(r => r.Id != id && r.IsDeleted == false)
                .Any(r => r.RoomNum == name);
        }

        public bool GetRoomNameForAdd(string name)
        {
            var hotel = GetHotel();

            return this.db
                .Rooms
                .Where(r => r.IsDeleted == false && r.HotelId == hotel.Id)
                .Any(r => r.RoomNum == name);
        }

        public async Task Update(EditRoomFormModel room)
        {
            var currentRoom = this.GetRoom(room.Id);
            var currentHotel = this.GetHotel();

            currentRoom.Description = room.Description;
            currentRoom.FloorNum = room.FloorNum;
            currentRoom.RoomNum = room.RoomNum;
            currentRoom.RoomTypeId = room.CurrentRoomTypeId;
            currentRoom.Hotel = currentHotel;

            this.db.Rooms.Update(currentRoom);
            await this.db.SaveChangesAsync();
         
        }

        public async Task Add(AddRoomFormModel room)
        {
            var newRoom = new Room
            {
                IsDeleted = false,
                Description = room.Description,
                FloorNum = room.FloorNum,
                HotelId = room.HotelId,
                RoomNum = room.RoomNum,
                RoomTypeId = room.RoomTypeId,
            };

            await this.db.Rooms.AddAsync(newRoom);
            await this.db.SaveChangesAsync();
        }

        public AddRoomFormModel FillRoomAddForm()
        {
            var currentHotel = this.GetHotel();
            var roomTypes = this.GetRoomTypes();

            var room = new AddRoomFormModel
            {
                HotelName = currentHotel.Name,
                HotelId = currentHotel.Id,
                RoomTypes = roomTypes
            };

            return room;
        }

        public async Task Delete(string id)
        {
            var room = this.db
                .Rooms
                .FirstOrDefault(r => r.Id == id);

            room.IsDeleted = true;

            await this.bookingService.CancelBooking(id);

            this.db.Rooms.Update(room);
            await this.db.SaveChangesAsync();
        }

        public Hotel GetHotel()
        {
            return this.db
                .Hotels
                .FirstOrDefault();
        }
    }
}
