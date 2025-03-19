using RoomServices.Models;

namespace RoomServices.Interface
{
    public interface IRoomService
    {

        IEnumerable<Room> GetRoomDetails();
        Room GetRoomById(int id);
        Room AddRoomDetails(Room room);
        Room UpdateRoomDetails(int id, Room updatedroomDetails);
        Room DeleteRoomById(int id);
        bool IsRoomAvailable(int id);
       Task<Room?> UpdateRoomStatus(int id, string status);
    }
}
