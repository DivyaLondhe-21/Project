using ReservationServices.Data;
using ReservationServices.Interface;
using ReservationServices.Models;
using RoomServices.Models;
using System.Text;
using System.Text.Json;

namespace ReservationServices.Repositories
{
    public class ReservationRepository : IReservation
    {
        private readonly HotelManagementSystemContext _context;
        private readonly HttpClient _httpClient;
        public ReservationRepository(HotelManagementSystemContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public IEnumerable<Reservation> GetReservations()
        {
            var reservation = _context.Reservations.ToList();
            return reservation;
        }

        public Reservation? GetReservationById(int id)
        {
            return _context.Reservations.FirstOrDefault(x => x.ReservationId == id);
        }


        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            
            //saving reservation
            reservation.ReservationId = 0;
            reservation.Status = "Pending";
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();


            var roomServiceUrl = $"http://roomservice/api/room/{reservation.RoomId}";
            var roomResponse = await _httpClient.GetAsync(roomServiceUrl);

            if (!roomResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve room details.");
            }

            var roomDetails = JsonSerializer.Deserialize<Room>(await roomResponse.Content.ReadAsStringAsync()); decimal pricePerNight = roomDetails.PricePerNight;
            decimal totalAmount = reservation.NumberOfNights * pricePerNight;


            //updating room status
            var roomUpdateServiceUrl = $"http://roomservice/api/room/{reservation.RoomId}/updateStatus";
            var content = new StringContent("\"Not Available\"", Encoding.UTF8, "application/json");
            var roomUpdateResponse = await _httpClient.PutAsync(roomUpdateServiceUrl, content);
            

            if (!roomUpdateResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update room status.");
            }
            
            var paymentServiceUrl = "http://paymentservice/api/payment/process";
            var paymentRequest = new
            {
                ReservationId = reservation.ReservationId,
                Amount = totalAmount, // Replace with actual calculation
                PayTime = DateTime.Now
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json");
            var paymentResponse = await _httpClient.PostAsync(paymentServiceUrl, jsonContent);
            if (!paymentResponse.IsSuccessStatusCode)
            {
                throw new Exception("Payment processing failed.");
            }
            reservation.Status = "Confirmed";
            await _context.SaveChangesAsync();
            return reservation;
            
        }

        public Reservation UpdateReservation(int id, Reservation updatedreservation)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.ReservationId == id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }
            reservation.NumberOfAdults = updatedreservation.NumberOfAdults;
            reservation.NumberOfChildren = updatedreservation.NumberOfChildren;
            reservation.CheckInDate = updatedreservation.CheckInDate;
            reservation.CheckOutDate = updatedreservation.CheckOutDate;
            reservation.Status = updatedreservation.Status;
            reservation.Discount = updatedreservation.Discount;

            _context.SaveChanges();
        
            if (reservation.Status == "Checked-Out")
            {
                // 🔹 Call Room Service to update room status to "Available"
                var roomServiceUrl = $"http://roomservice/api/room/{reservation.RoomId}/updateStatus";
                var roomUpdateResponse = _httpClient.PutAsync(roomServiceUrl, new StringContent("\"Available\"", Encoding.UTF8, "application/json")).Result;

                if (!roomUpdateResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to update room status.");
                }
            }
            

            return reservation;
        }

        public Reservation? DeleteReservation(int id)
        {
            
            var reservation = _context.Reservations.FirstOrDefault(x => x.ReservationId == id);
            if (reservation == null)
            {
                return null; //  Return null instead of throwing an exception
            }
            var roomServiceUrl = $"http://roomservice/api/room/{reservation.RoomId}/updateStatus";
            var roomUpdateResponse = _httpClient.PutAsync(roomServiceUrl, new StringContent("\"Available\"", Encoding.UTF8, "application/json")).Result;

            if (!roomUpdateResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update room status.");
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

            return reservation;
        
        }
    }
}
