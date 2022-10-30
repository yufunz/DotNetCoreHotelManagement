using IT703_A2.Models.RoomTypes;
using IT703_A2.Data;
using IT703_A2.Models;

namespace IT703_A2.Services
{
    public class RoomTypesService : IRoomTypesService
    {
        private readonly ApplicationDbContext db;

        public RoomTypesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Add(AddRoomTypeFormModel roomType)
        {
            var rType = new RoomType
            {
                Image = roomType.Image,
                Name = roomType.Name,
                NumOfBeds = roomType.NumOfBeds,
                Rate = roomType.Rate,
            };

            await this.db.RoomTypes.AddAsync(rType);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var rTypeForDelete = this.db.RoomTypes.FirstOrDefault(rt => rt.Id == id);

            rTypeForDelete.IsDeleted = true;

            this.db.RoomTypes.Update(rTypeForDelete);
            await this.db.SaveChangesAsync();

        }

        public EditRoomTypeFormModel GetRoomType(string id)
        {
            var a = this.db
                .RoomTypes
                .Where(t => t.Id == id)
                .Select(t => new EditRoomTypeFormModel
                {
                    Id = t.Id,
                    Image = t.Image,
                    Name = t.Name,
                    NumOfBeds = t.NumOfBeds,
                    Rate = t.Rate,
                }).FirstOrDefault();

            return a;
        }

        public bool IsRoomNameExistForAdd(string name)
        {
            return this.db
                .RoomTypes
                .Where(rt => rt.IsDeleted == false)
                .Any(r => r.Name == name);
        }

        public bool IsRoomNameExistForEdit(string name, string id)
        {
            return this.db
                .RoomTypes
                .Where(rt => rt.Id != id && rt.IsDeleted == false)
                .Any(rt => rt.Name == name);
        }

        public ListRoomTypeQueryModel ListTypes(ListRoomTypeQueryModel rTQuery)
        {
            var rtDb = this.db
                .RoomTypes
                .OrderBy(rt => rt.Rate)
                .Where(rt => rt.IsDeleted == false)
                .AsQueryable();

            var tPages = (int)Math.Ceiling((double)rtDb.ToList().Count / rTQuery.ItemsPerPage);

            if (rTQuery.CurrentPage > tPages)
            {
                rTQuery.CurrentPage = tPages;
            }

            if (rTQuery.CurrentPage <= 0)
            {
                rTQuery.CurrentPage = 1;
            }

            var allRoomTypes = rtDb
                .Skip((rTQuery.CurrentPage - 1) * rTQuery.ItemsPerPage)
                .Take(rTQuery.ItemsPerPage)
                .Select(t => new ListRoomTypeViewModel
                {
                    Id = t.Id,
                    Image = t.Image,
                    Name = t.Name,
                    NumOfBeds = t.NumOfBeds,
                    Rate = t.Rate,
                    RoomsCount = t.Rooms.Count
                }).ToList();

            var roomTypeQModel = new ListRoomTypeQueryModel
            {
                CurrentPage = rTQuery.CurrentPage,
                TotalPages = tPages,
                RoomTypes = allRoomTypes
            };

            return roomTypeQModel;
        }

        public async Task Update(EditRoomTypeFormModel roomType)
        {
            var currentRoomType = this.db
                .RoomTypes
                .Where(r => r.Id == roomType.Id)
                .FirstOrDefault();

            currentRoomType.Image = roomType.Image;
            currentRoomType.Name = roomType.Name;
            currentRoomType.NumOfBeds = roomType.NumOfBeds;
            currentRoomType.Rate = roomType.Rate;

            this.db.RoomTypes.Update(currentRoomType);
            await this.db.SaveChangesAsync();
        }

    }
}
