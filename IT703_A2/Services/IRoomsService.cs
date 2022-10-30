using IT703_A2.Models.Rooms;

namespace IT703_A2.Services
{
    public interface IRoomsService
    {
        ListRoomsQueryModel All(ListRoomsQueryModel rooms);

        DetailsRoomViewModel Details(string id);

        EditRoomFormModel Edit(string id);

        bool GetRoomNameForEdit(string name, string id);

        bool GetRoomNameForAdd(string id);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        Task Update(EditRoomFormModel room);

        Task Add(AddRoomFormModel room);

        AddRoomFormModel FillRoomAddForm();

        Task Delete(string id);
    }
}
