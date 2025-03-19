using GuestServices.Interface;
using GuestServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuest _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // Get all guests (Accessible by Owner, Manager, Receptionist)
        [Authorize(Roles = "Owner,Manager,Receptionist")]
        [HttpGet]
        public ActionResult<IEnumerable<Guest>> GetAllGuests()
        {
            var guests = _guestService.GetAllGuests();
            return Ok(guests);
        }

        // Get guest by ID
        [Authorize(Roles = "Owner,Manager,Receptionist")]
        [HttpGet("{id}")]
        public ActionResult<Guest> GetGuestById(int id)
        {
            var guest = _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            return Ok(guest);
        }

        [Authorize(Roles = "Owner,Manager,Receptionist")]
        [HttpGet("{id}/reservations")]
        public ActionResult<IEnumerable<Reservation>> GetGuestReservations(int id)
        {
            var guest = _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            return Ok(guest.Reservations)

        // Add a new guest (Accessible by Owner, Manager, Receptionist)
        [Authorize(Roles = "Owner,Manager,Receptionist")]
        [HttpPost]
        public ActionResult<Guest> AddGuest([FromBody] Guest guest)
        {
            var addedGuest = _guestService.AddGuest(guest);
            return CreatedAtAction(nameof(GetGuestById), new { id = addedGuest.GuestId }, addedGuest);
        }

        // Update guest details
        [Authorize(Roles = "Owner,Manager")]
        [HttpPut("{id}")]
        public ActionResult<Guest> UpdateGuest(int id, [FromBody] Guest updatedGuest)
        {
            var guest = _guestService.UpdateGuest(id, updatedGuest);
            if (guest == null)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            return Ok(guest);
        }

        // Delete guest
        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public ActionResult<Guest> DeleteGuest(int id)
        {
            var guest = _guestService.DeleteGuest(id);
            if (guest == null)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            return Ok(guest);
        }
    }
}
