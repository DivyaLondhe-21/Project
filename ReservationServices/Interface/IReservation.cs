using ReservationServices.Models;

namespace ReservationServices.Interface
{
    public interface IReservation
    {

        IEnumerable<Reservation> GetReservations();
        Reservation GetReservationById(int id);
        //Reservation CreateReservation(Reservation reservation);
        Task<Reservation> CreateReservation(Reservation reservation);
        Reservation UpdateReservation(int id, Reservation updatedReservation);
        Reservation DeleteReservation(int id);
    }
}
