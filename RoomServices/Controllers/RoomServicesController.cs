using RoomServices.Interface;
using RoomServices.Models;
using RoomServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using RoomServices.Data;

namespace RoomServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomServicesController : ControllerBase
    {
        private readonly IRoomService _roomservice;

        public RoomServicesController(IRoomService roomservices)
        {
            _roomservice = roomservices;
        }

        // GET: api/<HotelRoomServicesController>
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRoomDetails()
        {
            var roomDetails = _roomservice.GetRoomDetails();
            return Ok(roomDetails);
        }

        // GET api/<HotelRoomServicesController>/5
        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(int id)
        {
            var roomDetails = _roomservice.GetRoomById(id);
            if (roomDetails == null)
            {
                return NotFound($"Room with ID {id} not found.");
            }
            return Ok(roomDetails);
        }

        [HttpGet("availability/{id}")]
        public IActionResult CheckRoomAvailability(int id)
        {
            var room = _roomservice.GetRoomById(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found."); // ✅ Return 404
            }
            var isAvailable = _roomservice.IsRoomAvailable(id);
            return Ok(new { RoomId = id, Availability = isAvailable });
        }
        // POST api/<HotelRoomServicesController>
        [HttpPost]
        public ActionResult<Room> AddRoomDetails([FromBody] Room room)
        {
            var result = _roomservice.AddRoomDetails(room);
            if (result == null)
            {
                return BadRequest("A room with this Id already exists.");
            }
            return Ok(result);
        }

        // PUT api/<HotelRoomServicesController>/5
        [HttpPut("{id}")]
        public ActionResult<Room> UpdateRoomDetails(int id, [FromBody] Room updatedroomDetails)
        {
            var roomDetails = _roomservice.UpdateRoomDetails(id, updatedroomDetails);
            if (roomDetails == null)
            {
                return NotFound("Room not found");
            }
            return Ok(roomDetails);
        }

        [HttpPut("{id}/updateStatus")]
        public async Task<IActionResult> UpdateRoomStatus(int id, [FromBody] string status)
        {
            var result = _roomservice.UpdateRoomStatus(id, status);
            if (result == null)
            {
                return NotFound("Room not found.");
            }
            return Ok(result);
        }


        // DELETE api/<HotelRoomServicesController>/5
        [HttpDelete("{id}")]
        public ActionResult<Room> DeleteRoomById(int id)
        {
            var room = _roomservice.DeleteRoomById(id);
            if (room == null)
            {
                return NotFound($"Room with ID {id} not found."); // Return 404 instead of crashing
            }
            return Ok(room);
        }

    }
}