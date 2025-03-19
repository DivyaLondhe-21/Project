using RoomServices.Data;
using RoomServices.Interface;
using RoomServices.Models;
using Microsoft.EntityFrameworkCore;

namespace RoomServices.Repositories
{
    public class RoomServicesRepository : IRoomService
    {
        private readonly HotelManagementSystemContext _context;

        public RoomServicesRepository(HotelManagementSystemContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetRoomDetails()
        {
            var roomDetails = _context.Rooms.ToList();
            return roomDetails;
        }

        public Room? GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(x => x.RoomId == id);
        }

        public bool IsRoomAvailable(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            return room?.Availability ?? false;
        }

        public Room AddRoomDetails(Room room)
        {
            if (_context.Rooms.Any(r => r.RoomNumber == room.RoomNumber))
            {
                return null;    //  not allowed duplicate room numbers
            }

            room.RoomId = 0;
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return room;

        }

        public Room UpdateRoomDetails(int id, Room updatedRoom)
        {
            if (updatedRoom == null)
            {
                throw new ArgumentNullException(nameof(updatedRoom), "Updated room details cannot be null.");
            }
            var result = _context.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (result == null)
            {
                throw new Exception("Room not found.");
            }
            result.RoomNumber = updatedRoom.RoomNumber;
            result.RoomType = updatedRoom.RoomType;
            result.PricePerNight = updatedRoom.PricePerNight;
            result.Period = updatedRoom.Period;
            result.Availability = updatedRoom.Availability; //true or false
            if (updatedRoom.Status == "Available" || updatedRoom.Status == "Under Maintenance")
            {
                result.Status = updatedRoom.Status;
            }

            _context.SaveChanges();
            return result;
        }

        public async Task<Room?> UpdateRoomStatus(int id, string status)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return null;
            }

            room.Status = status;
            room.Availability = (status == "Available"); // Set availability based on status

            await _context.SaveChangesAsync();
            return room;
        }
        public Room? DeleteRoomById(int id)
        {
            //var roomdetails = _context.Rooms.Include(r => r.Reservations).FirstOrDefault(x => x.RoomId == id);
            var roomdetails = _context.Rooms.FirstOrDefault(x => x.RoomId == id);

            //if (roomdetails == null)
            //{
            //    return null; // Return null instead of throwing an exception
            //}
            //foreach (var reservation in roomdetails.Reservations)
            //{
            //    reservation.Status = "Canceled";
            //}
            _context.Rooms.Remove(roomdetails);
            _context.SaveChanges();

            return roomdetails;
        }
    }
}
