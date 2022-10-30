using IT703_A2.Models.RoomTypes;

namespace IT703_A2.Services
{
    public interface IRoomTypesService
    {
        ListRoomTypeQueryModel ListTypes(ListRoomTypeQueryModel rTQuery);

        EditRoomTypeFormModel GetRoomType(string id);

        Task Update(EditRoomTypeFormModel roomType);

        Task Add(AddRoomTypeFormModel roomType);

        bool IsRoomNameExistForAdd(string name);

        bool IsRoomNameExistForEdit(string name, string id);

        Task Delete(string id);
    }
}
