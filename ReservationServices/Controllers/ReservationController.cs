using ReservationServices.Interface;
using ReservationServices.Models;
using ReservationServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using ReservationServices.Data;

namespace ReservationServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation _reservationService;

        public ReservationController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/<HotelReservationController>
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()
        {
            var reservation = _reservationService.GetReservations();
            return Ok(reservation);
        }

        // GET api/<HotelReservationController>/5
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservationbyId(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }
            return Ok(reservation);
        }


        // POST api/<HotelReservationController>
        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservaion([FromBody] Reservation reservation)
        {
            var result = _reservationService.CreateReservation(reservation);
            if (result == null)
            {
                return BadRequest("A reservation with this Id already exists.");
            }
            return Ok(result);
        }

        // PUT api/<HotelReservationController>/5
        [HttpPut("{id}")]
        public ActionResult<Reservation> UpdateReservation(int id, [FromBody] Reservation updatedreservation)
        {
            var reservation = _reservationService.UpdateReservation(id, updatedreservation);
            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }
            return Ok(reservation);
        }

        // DELETE api/<HotelReservationController>/5
        [HttpDelete("{id}")]
        public ActionResult<Reservation> DeleteReservationbyid(int id)
        {
            var reservation = _reservationService.DeleteReservation(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found."); // Return 404 instead of crashing
            }
            return Ok(reservation);
        }

    }
}